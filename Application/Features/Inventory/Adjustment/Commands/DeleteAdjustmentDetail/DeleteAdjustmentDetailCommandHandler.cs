using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Domain.Orders;
using Domain.Adjustments;

namespace Contract.Features.Transactions.Order.Commands.DeleteOrderDetail
{
    public sealed class DeleteAdjustmentDetailCommandHandler : IRequestHandler<DeleteAdjustmentDetailCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteAdjustmentDetailCommandHandler> _logger; 

        public DeleteAdjustmentDetailCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteAdjustmentDetailCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteAdjustmentDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteAdjustmentDetailCommandHandler));

            var entity = await _context.AdjustmentDetails
                .Include(o => o.Adjustment)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteAdjustmentDetailCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"AdjustmentDetail.NotFound\", \"AdjustmentDetail was not found.\")");
                return Error.NotFound("AdjustmentDetail.NotFound", "AdjustmentDetail was not found.");

            }

            if (entity.Adjustment!.IsLocked)

            {

                _logger.LogError("DeleteAdjustmentDetailCommandHandler stopped because an error result was returned: {ErrorResult}.", "AdjustmentErrors.AdjusmentIsLocked");
                return AdjustmentErrors.AdjusmentIsLocked;

            }

            _logger.LogInformation("DeleteAdjustmentDetailCommandHandler is marking entity data for persistence operation.");
            _context.AdjustmentDetails.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteAdjustmentDetailCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.AdjustmentDetail), cancellationToken);
            _logger.LogInformation("DeleteAdjustmentDetailCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("AdjustmentDetails deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

