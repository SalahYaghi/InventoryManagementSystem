using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Adjustments.DTOs;
using Contract.Features.Inventory.Adjustments.Mappers;
using Domain.Adjustments;
using Domain.Orders;
using Domain.Warehouses;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Adjustments.Commands.CreateAdjustment
{
    public sealed class CreateAdjustmentCommandHandler : IRequestHandler<CreateAdjustmentCommand, Result<AdjustmentDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateAdjustmentCommandHandler> _logger;
        private readonly IOrderPolicies _orderPolicies;

        public CreateAdjustmentCommandHandler(
            IAppDbContext context,
            ICachingService cache,
              IOrderPolicies orderPolicies,
            
            ILogger<CreateAdjustmentCommandHandler> logger)
        {
            _orderPolicies = orderPolicies;
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<AdjustmentDto>> Handle(CreateAdjustmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateAdjustmentCommandHandler));


            List<AdjustmentDetail> adjusmentDetails = new();


            var productIds = request.AdjustmentDetailCommands.Select(p => p.ProductId).ToList();

            var rowVersions = await _context.WarehouseStocks
                .Where(p => productIds.Contains(p.ProductId) && p.WarehouseId == request.WarehouseId)
                .Select(p => new { p.ProductId, p.RowVersion })
                .ToDictionaryAsync(x => x.ProductId, x => x.RowVersion, cancellationToken);

            foreach (var detail in request.AdjustmentDetailCommands)
            {
                if (!rowVersions.TryGetValue(detail.ProductId, out var storedRowVersion))
                {
                    _logger.LogWarning(
                        "CreateAdjustmentCommandHandler stopped: product {ProductId} has no stock row in warehouse {WarehouseId}.",
                        detail.ProductId, request.WarehouseId);
                    return ApplicationErrors.WarehouseStockNotFound;
                }

                if (storedRowVersion is null || !storedRowVersion.SequenceEqual(detail.RowVersion))
                {
                    _logger.LogWarning(
                        "CreateAdjustmentCommandHandler stopped: stale row version for product {ProductId}.",
                        detail.ProductId);
                    return ApplicationErrors.UpdateOccursOnProducts;
                }

                var detailObject = AdjustmentDetail.Create(Guid.NewGuid(),
                    detail.ProductId, detail.Quantity);

                if (detailObject.IsError)

                {

                    _logger.LogError("CreateAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "detailObject.Errors");
                    return detailObject.Errors;

                }

                adjusmentDetails.Add(detailObject.Value);
            }

            var warehouseStatusSource = await _context.Warehouses
          .Where(s => s.Id == request.WarehouseId)
          .Select(s => (bool?)(s.WarehouseStatus == WarehouseStatus.Active))
          .FirstOrDefaultAsync(cancellationToken);

            if (warehouseStatusSource is null)

            {

                _logger.LogWarning("CreateAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.WarehouseNotFound");
                return ApplicationErrors.WarehouseNotFound;

            }
            if (!warehouseStatusSource.Value)
            {
                _logger.LogWarning("CreateAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.WarehouseInActive");
                return ApplicationErrors.WarehouseInActive;
            }


            var entityResult = Domain.Adjustments.Adjustment.Create(Guid.NewGuid(),
                request.WarehouseId, request.AdjustmentReason, adjusmentDetails,
                request.AdjustmentType, 
                 request.Notes );

            if (entityResult.IsError)

            {

                _logger.LogError("CreateAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            if (entityResult.Value.AdjustmentType == AdjustmentType.Decrease) {

                foreach (var entity in entityResult.Value.AdjustmentDetails) {

                    var result = await _orderPolicies.CheckPrductAvailableQuantity(
                        entityResult.Value.WarehouseId, entity.ProductId, entity.Quantity, cancellationToken);
                    if (result.IsError)
                    {
                        _logger.LogError("CreateAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                        return result.Errors;
                    }

                }



            }
           

            _logger.LogInformation("CreateAdjustmentCommandHandler is adding new entity data to the context.");
            await _context.AdjustmentDetails.AddRangeAsync(adjusmentDetails);
            await _context.Adjustments.AddAsync(entityResult.Value);
            _logger.LogInformation("CreateAdjustmentCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateAdjustmentCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Adjustment), cancellationToken);

            _logger.LogInformation("Adjustment created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

