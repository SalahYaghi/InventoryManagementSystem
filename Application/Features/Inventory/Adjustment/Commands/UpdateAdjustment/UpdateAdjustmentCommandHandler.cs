using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Adjustments.DTOs;
using Contract.Features.Inventory.Adjustments.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;
using Contract.Common.Errors;
using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustment;

namespace Contract.Features.Inventory.Adjustments.Commands.UpdateAdjustment
{
    public sealed class UpdateAdjustmentCommandHandler : IRequestHandler<UpdateAdjustmentCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateAdjustmentCommandHandler> _logger;

        public UpdateAdjustmentCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateAdjustmentCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Updated>> Handle(UpdateAdjustmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateAdjustmentCommandHandler));

            var entity = await _context.Adjustments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateAdjustmentCommandHandler stopped: adjustment {Id} not found.", request.Id);
                return ApplicationErrors.AdjustmentNotFound;

            }
      
            var result = entity.Update(request.Notes);

            if (result.IsError)

            {

                _logger.LogError("UpdateAdjustmentCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _logger.LogInformation("UpdateAdjustmentCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateAdjustmentCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Adjustment), cancellationToken);

            _logger.LogInformation("Adjustment updated successfully with key {Key}", request.Id);

            return Result.Updated;
        }
    }
}

