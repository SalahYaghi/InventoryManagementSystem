using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Inventory.Adjustment.Mappers;
using Domain.Adjustments;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Order.Commands.CreateOrderDetail
{
    public class CreateAdjustmentDetailCommandHandler(
        IAppDbContext context,
        IOrderPolicies orderPolicies,
        ICachingService cache,
        ILogger<CreateAdjustmentDetailCommandHandler> logger)
        : IRequestHandler<CreateAdjustmentDetailCommand, Result<AdjustmentDetailDto>>
    {
        private readonly ILogger<CreateAdjustmentDetailCommandHandler> _logger = logger;

        public async Task<Result<AdjustmentDetailDto>> Handle(
            CreateAdjustmentDetailCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateAdjustmentDetailCommandHandler));

            var entity = await context.Adjustments
                .Include(o => o.AdjustmentDetails)
                .FirstOrDefaultAsync(x => x.Id == request.AdjustmentId, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("CreateAdjustmentDetailCommandHandler stopped: adjustment {Id} not found.", request.AdjustmentId);
                return ApplicationErrors.AdjustmentNotFound;
            }

            if (entity.IsLocked)
            {
                _logger.LogWarning("CreateAdjustmentDetailCommandHandler stopped: adjustment {Id} is locked.", request.AdjustmentId);
                return AdjustmentErrors.AdjusmentIsLocked;
            }

            if (entity.AdjustmentDetails.Any(o => o.ProductId == request.ProductId))
            {
                _logger.LogWarning("CreateAdjustmentDetailCommandHandler stopped: product already on the adjustment.");
                return ApplicationErrors.ProductAlreadyExistInOrderDetails;
            }

            var rowVersion = await context.WarehouseStocks
                .Where(p => p.ProductId == request.ProductId && p.WarehouseId == entity.WarehouseId)
                .Select(p => p.RowVersion)
                .FirstOrDefaultAsync(cancellationToken);

            if (rowVersion is null)
            {
                _logger.LogWarning(
                    "CreateAdjustmentDetailCommandHandler stopped: product {ProductId} has no stock row in warehouse {WarehouseId}.",
                    request.ProductId, entity.WarehouseId);
                return ApplicationErrors.WarehouseStockNotFound;
            }

            if (!rowVersion.SequenceEqual(request.RowVersion))
            {
                _logger.LogWarning("CreateAdjustmentDetailCommandHandler stopped: stale row version for product {ProductId}.", request.ProductId);
                return ApplicationErrors.UpdateOccursOnProducts;
            }

            var detailedResult = AdjustmentDetail.Create(Guid.NewGuid(), request.ProductId, request.Quantity);

            if (detailedResult.IsError)
            {
                _logger.LogError("CreateAdjustmentDetailCommandHandler stopped: {Errors}", detailedResult.Errors);
                return detailedResult.Errors;
            }

            if (entity.AdjustmentType == AdjustmentType.Decrease)
            {
                var availability = await orderPolicies.CheckPrductAvailableQuantity(
                    entity.WarehouseId, request.ProductId, request.Quantity, cancellationToken);

                if (availability.IsError)
                {
                    _logger.LogError("CreateAdjustmentDetailCommandHandler stopped: {Errors}", availability.Errors);
                    return availability.Errors;
                }
            }

            var addResult = entity.AddAdjustmentDetail(detailedResult.Value);

            if (addResult.IsError)
            {
                _logger.LogError("CreateAdjustmentDetailCommandHandler stopped: {Errors}", addResult.Errors);
                return addResult.Errors;
            }

            await context.AdjustmentDetails.AddAsync(detailedResult.Value, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.AdjustmentDetail), cancellationToken);

            _logger.LogInformation("CreateAdjustmentDetailCommandHandler completed successfully.");
            return detailedResult.Value.ToDto();
        }
    }
}
