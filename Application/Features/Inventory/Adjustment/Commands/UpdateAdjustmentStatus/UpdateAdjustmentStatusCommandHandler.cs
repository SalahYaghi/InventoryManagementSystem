using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Domain.Adjustments;
using Domain.Warehouses;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed class UpdateAdjustmentStatusCommandHandler : IRequestHandler<UpdateAdjustmentStatusCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;

        private readonly ILogger<UpdateAdjustmentStatusCommandHandler> _logger;

        public UpdateAdjustmentStatusCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateAdjustmentStatusCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Updated>> Handle(UpdateAdjustmentStatusCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateAdjustmentStatusCommandHandler));

            var entity = await _context.Adjustments
                .Include(o => o.AdjustmentDetails)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("UpdateAdjustmentStatusCommandHandler stopped: adjustment {Id} not found.", request.Id);
                return ApplicationErrors.AdjustmentNotFound;
            }

            if (entity.IsLocked)
            {
                _logger.LogWarning("UpdateAdjustmentStatusCommandHandler stopped: adjustment {Id} is locked.", request.Id);
                return AdjustmentErrors.AdjusmentIsLocked;
            }

            if (request.AdjustmentStatus == AdjustmentStatus.Approved)
            {
                var movement = await ApplyStockMovementsAsync(entity, cancellationToken);
                if (movement.IsError) return movement.Errors;
            }

            var result = entity.UpdateStatus(request.AdjustmentStatus);

            if (result.IsError)
            {
                _logger.LogError("UpdateAdjustmentStatusCommandHandler stopped: {Errors}", result.Errors);
                return result.Errors;
            }

            _logger.LogInformation("UpdateAdjustmentStatusCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Adjustment), cancellationToken);

            _logger.LogInformation("Adjustment updated successfully with key {Key}", request.Id);

            return Result.Updated;
        }

        private async Task<Result<Success>> ApplyStockMovementsAsync(Adjustment entity, CancellationToken ct)
        {
            var productIds = entity.AdjustmentDetails.Select(o => o.ProductId).ToList();

            var stock = await _context.WarehouseStocks
                .Where(w => w.WarehouseId == entity.WarehouseId && productIds.Contains(w.ProductId))
                .ToListAsync(ct);

            foreach (var detail in entity.AdjustmentDetails)
            {
                var currentStock = stock.FirstOrDefault(w => w.ProductId == detail.ProductId);

                if (currentStock is null)
                {
                    if (entity.AdjustmentType == AdjustmentType.Increase)
                    {
                        var created = WarehouseStock.Create(
                            Guid.NewGuid(), entity.WarehouseId, detail.ProductId, 0, detail.Quantity);

                        if (created.IsError)
                        {
                            _logger.LogError("UpdateAdjustmentStatusCommandHandler stopped: {Errors}", created.Errors);
                            return created.Errors;
                        }

                        await _context.WarehouseStocks.AddAsync(created.Value, ct);
                        stock.Add(created.Value);
                        continue;
                    }

                    _logger.LogError(
                        "UpdateAdjustmentStatusCommandHandler stopped: cannot decrease product {ProductId}, no stock row in warehouse {WarehouseId}.",
                        detail.ProductId, entity.WarehouseId);

                    return ApplicationErrors.WarehouseStockNotFound;
                }

                switch (entity.AdjustmentType)
                {
                    case AdjustmentType.Increase:
                    {
                        var updateResult = currentStock.AddToQuantity(detail.Quantity);
                        if (updateResult.IsError)
                        {
                            _logger.LogError("UpdateAdjustmentStatusCommandHandler stopped: {Errors}", updateResult.Errors);
                            return updateResult.Errors;
                        }
                        break;
                    }

                    case AdjustmentType.Decrease:
                    {
                        var updateResult = currentStock.RemoveQuantity(detail.Quantity);
                        if (updateResult.IsError)
                        {
                            _logger.LogError("UpdateAdjustmentStatusCommandHandler stopped: {Errors}", updateResult.Errors);
                            return updateResult.Errors;
                        }
                        break;
                    }

                    default:
                        _logger.LogError("UpdateAdjustmentStatusCommandHandler stopped: unsupported adjustment type {Type}.", entity.AdjustmentType);
                        return ApplicationErrors.UnsupportedAdjustmentType;
                }
            }

            return Result.Success;
        }
    }
}
