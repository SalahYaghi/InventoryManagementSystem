using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustment;
using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustmentDetailsQuantity;
 using Contract.Features.Transactions.Order.DTOs;
using Contract.Features.Transactions.Order.Mappers;
using Domain.Adjustments;
using Contract.Common.Errors;
using Domain.Orders;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Transactions.Order.Commands.UpdateOrderDetail
{
    public sealed class UpdateAdjustmentDetailQuantityCommandHandler : IRequestHandler<UpdateAdjustmentDetailQuantityCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateAdjustmentDetailQuantityCommandHandler> _logger; // [FIX 3.8] was typed to the COMMAND
        private readonly IOrderPolicies _orderPolicies;

        public UpdateAdjustmentDetailQuantityCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateAdjustmentDetailQuantityCommandHandler> logger,
            IOrderPolicies orderPolicies)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _orderPolicies = orderPolicies;

        }

        public async Task<Result<Updated>> Handle(UpdateAdjustmentDetailQuantityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateAdjustmentDetailQuantityCommandHandler));

            var entity = await _context.AdjustmentDetails
                .Include(o => o.Adjustment)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("UpdateAdjustmentDetailQuantityCommandHandler stopped: adjustment detail {Id} not found.", request.Id);
                return ApplicationErrors.AdjustmentDetailNotFound;
            }

            if (entity.RowVersion is null || !entity.RowVersion.SequenceEqual(request.RowVersion))
            {
                _logger.LogWarning("UpdateAdjustmentDetailQuantityCommandHandler stopped: stale row version for detail {Id}.", request.Id);
                return ApplicationErrors.UpdateOccursOnProducts;
            }

            if (entity.Adjustment!.IsLocked)
            {
                _logger.LogWarning("UpdateAdjustmentDetailQuantityCommandHandler stopped: adjustment is locked.");
                return AdjustmentErrors.AdjusmentIsLocked;
            }



            var netQuantity = request.Quantity - entity.Quantity;

            if (netQuantity == 0)
            {
                _logger.LogInformation("UpdateAdjustmentDetailQuantityCommandHandler: quantity unchanged, nothing to do.");
                return Result.Updated;
            }

            if (netQuantity > 0 && entity.Adjustment.AdjustmentType == AdjustmentType.Decrease)
            {
                var checkResult = await _orderPolicies.CheckPrductAvailableQuantity(
                    entity.Adjustment.WarehouseId, entity.ProductId, netQuantity, cancellationToken);

                if (checkResult.IsError)
                {
                    _logger.LogError("UpdateAdjustmentDetailQuantityCommandHandler stopped: {Errors}", checkResult.Errors);
                    return checkResult.Errors;
                }
            }

            var result = entity.UpdateQuantity(request.Quantity);

            if (result.IsError)
            {
                _logger.LogError("UpdateAdjustmentDetailQuantityCommandHandler stopped: {Errors}", result.Errors);
                return result.Errors;
            }


            _logger.LogInformation("UpdateAdjustmentDetailQuantityCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateAdjustmentDetailQuantityCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.AdjustmentDetail), cancellationToken);

            _logger.LogInformation("AdjustmentDetail updated successfully with key {Key}", request.Id);

            return Result.Updated;
        }
    }
}

