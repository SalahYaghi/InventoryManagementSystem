using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Domain.Adjustments;
using Domain.Orders;

namespace Contract.Features.Inventory.Adjustments.Commands.DeleteAdjustment
{
    public sealed class DeleteAdjustmentCommandHandler : IRequestHandler<DeleteAdjustmentCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteAdjustmentCommandHandler> _logger;

        public DeleteAdjustmentCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteAdjustmentCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteAdjustmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteAdjustmentCommandHandler));

            var entity = await _context.Adjustments
                .Include(a => a.AdjustmentDetails)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Adjustment.NotFound\", \"Adjustment was not found.\")");
                return Error.NotFound("Adjustment.NotFound", "Adjustment was not found.");

            }

            if (entity.IsLocked)

            {

                _logger.LogError("DeleteAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "AdjustmentErrors.AdjusmentIsLocked");
                return AdjustmentErrors.AdjusmentIsLocked;

            }

            _context.AdjustmentDetails.RemoveRange(entity.AdjustmentDetails!);
            _logger.LogInformation("DeleteAdjustmentCommandHandler is marking entity data for persistence operation.");
            _context.Adjustments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteAdjustmentCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Adjustment), cancellationToken);
            _logger.LogInformation("DeleteAdjustmentCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Adjustment deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

