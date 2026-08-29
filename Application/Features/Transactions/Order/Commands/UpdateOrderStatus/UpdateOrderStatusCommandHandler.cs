using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.Orders.Mappers;
using Domain.Orders;
using Domain.Orders.Events;
using Domain.Warehouses;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.Design;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result<OrderDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateOrderStatusCommandHandler> _logger;

        public UpdateOrderStatusCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateOrderStatusCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<OrderDto>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateOrderStatusCommandHandler));

            var entity = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("UpdateOrderStatusCommandHandler stopped: order {OrderId} not found.", request.Id);
                return ApplicationErrors.OrderNotFound;
            }

            if (entity.IsLocked)
            {
                _logger.LogWarning("UpdateOrderStatusCommandHandler stopped: order is locked.");
                return OrderErrors.OrderIsLocked;
            }

            var result = entity.UpdateStatus(request.OrderStatus);

            if (result.IsError)
            {
                _logger.LogError("UpdateOrderStatusCommandHandler stopped: {Errors}", result.Errors);
                return result.Errors;
            }

            if (entity.OrderStatus == OrderStatus.Completed)
            {
                var movement = await ApplyStockMovementsAsync(entity, cancellationToken);
                if (movement.IsError) return movement.Errors;

                entity.AddDomainEvent(new OrderCompeletedEvent { OrderId = entity.Id });
            }

            _logger.LogInformation("UpdateOrderStatusCommandHandler is saving changes to the database.");

            if (entity.OrderStatus == OrderStatus.Completed) {
            
                for (int attempt = 0; attempt <= 3; attempt++) {
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        break;
                    }
                    catch (DbUpdateConcurrencyException ex) when (attempt < 3)
                    {
                        foreach (var entry in ex.Entries) {
                            await entry.ReloadAsync(cancellationToken);
                        }
                        var movement = await ApplyStockMovementsAsync(entity, cancellationToken);
                        if (movement.IsError) return movement.Errors;
                    }
                }
           
            }else {
                await _context.SaveChangesAsync(cancellationToken);
            }

            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Order), cancellationToken);

            _logger.LogInformation("Order updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }

        

        private async Task<Result<Success>> ApplyStockMovementsAsync(Domain.Orders.Order entity, CancellationToken ct)
        {
            var productIds = entity.OrderDetails.Select(o => o.ProductId).ToHashSet();

            var sourceStock = await _context.WarehouseStocks
                .Where(w => w.WarehouseId == entity.SourceWarehouseId && productIds.Contains(w.ProductId))
                .ToListAsync(ct);

            var destinationStock = new List<WarehouseStock>();

            if (entity.OrderType == OrderType.Transfer)
            {
                destinationStock = await _context.WarehouseStocks
                    .Where(w => w.WarehouseId == entity.DestinationWarehouseId && productIds.Contains(w.ProductId))
                    .ToListAsync(ct);
            }

            foreach (var detail in entity.OrderDetails)
            {
                var currentStock = sourceStock.FirstOrDefault(w => w.ProductId == detail.ProductId);

                if (currentStock is null)
                {
                    if (entity.OrderType is OrderType.Purchase or OrderType.ReturnIn)
                    {
                        var newStock = WarehouseStock.Create(
                            Guid.NewGuid(), entity.SourceWarehouseId, detail.ProductId, 0, detail.Quantity);

                        if (newStock.IsError)
                        {
                            _logger.LogError("UpdateOrderStatusCommandHandler stopped: {Errors}", newStock.Errors);
                            return newStock.Errors;
                        }

                        await _context.WarehouseStocks.AddAsync(newStock.Value, ct);
                        sourceStock.Add(newStock.Value);
                        continue;
                    }

                    _logger.LogError(
                        "UpdateOrderStatusCommandHandler stopped: no stock row for product {ProductId} in source warehouse {WarehouseId}; " +
                        "cannot complete a {OrderType} order.",
                        detail.ProductId, entity.SourceWarehouseId, entity.OrderType);

                    return ApplicationErrors.WarehouseStockNotFound;
                }

                switch (entity.OrderType)
                {
                    case OrderType.Purchase:
                    case OrderType.ReturnIn:
                    {
                        var updateResult = currentStock.AddToQuantity(detail.Quantity);
                        if (updateResult.IsError)
                        {
                            _logger.LogError("UpdateOrderStatusCommandHandler stopped: {Errors}", updateResult.Errors);
                            return updateResult.Errors;
                        }
                        break;
                    }

                    case OrderType.Sale:
                    case OrderType.ReturnOut:
                    {
                        var updateResult = currentStock.RemoveQuantity(detail.Quantity);
                        if (updateResult.IsError)
                        {
                            _logger.LogError("UpdateOrderStatusCommandHandler stopped: {Errors}", updateResult.Errors);
                            return updateResult.Errors;
                        }
                        break;
                    }

                    case OrderType.Transfer:
                    {
                        var removeResult = currentStock.RemoveQuantity(detail.Quantity);
                        if (removeResult.IsError)
                        {
                            _logger.LogError("UpdateOrderStatusCommandHandler stopped: {Errors}", removeResult.Errors);
                            return removeResult.Errors;
                        }

                        var currentDestinationStock = destinationStock.FirstOrDefault(w => w.ProductId == detail.ProductId);

                        if (currentDestinationStock is null)
                        {
                            var newStock = WarehouseStock.Create(
                                Guid.NewGuid(),
                                entity.DestinationWarehouseId!.Value,
                                detail.ProductId,
                                currentStock.MinimumStockLevel);

                            if (newStock.IsError)
                            {
                                _logger.LogError("UpdateOrderStatusCommandHandler stopped: {Errors}", newStock.Errors);
                                return newStock.Errors;
                            }

                            currentDestinationStock = newStock.Value;
                            await _context.WarehouseStocks.AddAsync(currentDestinationStock, ct);

                            destinationStock.Add(currentDestinationStock);
                        }

                        var addResult = currentDestinationStock.AddToQuantity(detail.Quantity);
                        if (addResult.IsError)
                        {
                            _logger.LogError("UpdateOrderStatusCommandHandler stopped: {Errors}", addResult.Errors);
                            return addResult.Errors;
                        }
                        break;
                    }

                    default:
                        _logger.LogError("UpdateOrderStatusCommandHandler stopped: unsupported order type {OrderType}.", entity.OrderType);
                        return ApplicationErrors.UnsupportedOrderType;
                }
            }

            return Result.Success;
        }
    }
}
