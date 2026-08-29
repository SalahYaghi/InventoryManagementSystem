using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Transactions.Order.DTOs;
using Contract.Features.Transactions.Order.Mappers;
using Domain.Orders;
using Domain.Warehouses;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;

namespace Contract.Features.Transactions.Order.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommandHandler(
        IAppDbContext context,
        IProductPolicies productPolicies,
        IOrderPolicies orderPolicies,
        ILogger<CreateOrderDetailCommandHandler> logger,
        ICachingService cache) : IRequestHandler<CreateOrderDetailCommand, Result<OrderDetailDto>>
    {
        private readonly ILogger<CreateOrderDetailCommandHandler> _logger = logger;

        public async Task<Result<OrderDetailDto>> Handle(
            CreateOrderDetailCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateOrderDetailCommandHandler));

            var entity = await context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("CreateOrderDetailCommandHandler stopped: order {OrderId} not found.", request.OrderId);
                return ApplicationErrors.OrderNotFound;
            }

            if (entity.OrderDetails.Any(o => o.ProductId == request.ProductId))
            {
                _logger.LogWarning("CreateOrderDetailCommandHandler stopped: product already on the order.");
                return ApplicationErrors.ProductAlreadyExistInOrderDetails;
            }

            byte[]? rowVersion;
            decimal unitPrice;

            if (entity.OrderType != OrderType.Purchase && entity.OrderType != OrderType.ReturnOut)
            {
                var stock = await context.WarehouseStocks
                    .Include(r => r.Product)
                    .Where(p => p.ProductId == request.ProductId && p.WarehouseId == entity.SourceWarehouseId)
                  
                    .FirstOrDefaultAsync(cancellationToken);

                if (stock is null)
                {
                    _logger.LogWarning(
                        "CreateOrderDetailCommandHandler stopped: no stock row for product {ProductId} in warehouse {WarehouseId}.",
                        request.ProductId, entity.SourceWarehouseId);
                    return ApplicationErrors.WarehouseStockNotFound;
                }

                context.Entry(stock).Property(r => r.LastModifiedUtc).IsModified = true;
                rowVersion = stock.RowVersion;
                unitPrice = stock.Product!.SellingPrice;
            }
            else
            {
                var supplierProduct = await context.SupplierProducts
                    .Where(p => p.ProductId == request.ProductId && p.SupplierId == entity.SupplierId)
                    .Select(p => new { UnitPrice = p.PurchasePrice, p.RowVersion })
                    .FirstOrDefaultAsync(cancellationToken);

                if (supplierProduct is null)
                {
                    _logger.LogWarning("CreateOrderDetailCommandHandler stopped: supplier does not sell product {ProductId}.", request.ProductId);
                    return ApplicationErrors.SupplierDoesNotSellProduct;
                }

                rowVersion = supplierProduct.RowVersion;
                unitPrice = supplierProduct.UnitPrice;
            }

            if (rowVersion is null || !rowVersion.SequenceEqual(request.RowVersion))
            {
                _logger.LogWarning("CreateOrderDetailCommandHandler stopped: stale row version.");
                return ApplicationErrors.UpdateOccursOnProducts;
            }

            var detailedResult = OrderDetail.Create(Guid.NewGuid(), request.ProductId, request.Quantity, unitPrice);

            if (detailedResult.IsError)
            {
                _logger.LogError("CreateOrderDetailCommandHandler stopped: {Errors}", detailedResult.Errors);
                return detailedResult.Errors;
            }

            var validation = await ValidateForOrderTypeAsync(entity, request, cancellationToken);
            if (validation.IsError) return validation.Errors;

            var addResult = entity.AddOrderDetail(detailedResult.Value);
            if (addResult.IsError)
            {
                _logger.LogError("CreateOrderDetailCommandHandler stopped: {Errors}", addResult.Errors);
                return addResult.Errors;
            }

            await context.OrderDetails.AddAsync(detailedResult.Value, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.OrderDetail), cancellationToken);

            _logger.LogInformation("CreateOrderDetailCommandHandler completed successfully.");
            return detailedResult.Value.ToDto();
        }

        private async Task<Result<Success>> ValidateForOrderTypeAsync(
            Domain.Orders.Order entity,
            CreateOrderDetailCommand request,
            CancellationToken ct)
        {
            switch (entity.OrderType)
            {
                case OrderType.Purchase:
                    return await ValidateSupplierAsync(entity, request, ct);

                case OrderType.ReturnOut:
                {
                    var supplier = await ValidateSupplierAsync(entity, request, ct);
                    if (supplier.IsError) return supplier.Errors;

                    return await ValidateAvailabilityAsync(entity, request, ct);
                }

                case OrderType.Sale:
                {
                    var customer = await ValidateCustomerAsync(entity, ct);
                    if (customer.IsError) return customer.Errors;

                    return await ValidateAvailabilityAsync(entity, request, ct);
                }

                case OrderType.ReturnIn:
                    return await ValidateCustomerAsync(entity, ct);

                case OrderType.Transfer:
                {
                    var destinationActive = await context.Warehouses
                        .Where(s => s.Id == entity.DestinationWarehouseId)
                        .Select(s => (bool?)(s.WarehouseStatus == WarehouseStatus.Active))
                        .FirstOrDefaultAsync(ct);

                    if (destinationActive is null) return ApplicationErrors.WarehouseNotFound;
                    if (!destinationActive.Value) return ApplicationErrors.WarehouseInActive;

                    return await ValidateAvailabilityAsync(entity, request, ct);
                }

                default:
                    _logger.LogError("CreateOrderDetailCommandHandler stopped: unsupported order type {OrderType}.", entity.OrderType);
                    return ApplicationErrors.UnsupportedOrderType;
            }
        }

        private async Task<Result<Success>> ValidateSupplierAsync(
            Domain.Orders.Order entity, CreateOrderDetailCommand request, CancellationToken ct)
        {
            var supplierStatus = await context.Suppliers
                .Where(s => s.Id == entity.SupplierId)
                .Select(s => (bool?)s.Status)
                .FirstOrDefaultAsync(ct);

            if (supplierStatus is null) return ApplicationErrors.SupplierNotFound;
            if (!supplierStatus.Value) return ApplicationErrors.SupplierInActive;

            var sells = await productPolicies.CheckSupplierSellsProducts(
                entity.SupplierId!.Value, [request.ProductId], ct);

            if (sells.IsError) return sells.Errors;
            return Result.Success;
        }

        private async Task<Result<Success>> ValidateCustomerAsync(Domain.Orders.Order entity, CancellationToken ct)
        {
            var exists = await context.Customers.AnyAsync(s => s.Id == entity.CustomerId, ct);

            if (!exists) return ApplicationErrors.CustomerNotFound;
            return Result.Success;
        }

        private async Task<Result<Success>> ValidateAvailabilityAsync(
            Domain.Orders.Order entity, CreateOrderDetailCommand request, CancellationToken ct)
        {
            var result = await orderPolicies.CheckPrductAvailableQuantity(
                entity.SourceWarehouseId, request.ProductId, request.Quantity, ct);

            if (result.IsError) return result.Errors;
            return Result.Success;
        }
    }
}
