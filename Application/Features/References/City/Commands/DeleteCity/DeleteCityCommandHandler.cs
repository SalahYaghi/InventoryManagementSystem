using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.Cities.Commands.DeleteCity
{
    public sealed class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteCityCommandHandler> _logger;

        public DeleteCityCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteCityCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteCityCommandHandler));

            var entity = await _context.Cities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteCityCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"City.NotFound\", \"City was not found.\")");
                return Error.NotFound("City.NotFound", "City was not found.");

            }

            _logger.LogInformation("DeleteCityCommandHandler is marking entity data for persistence operation.");
            _context.Cities.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteCityCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.City), cancellationToken);
            _logger.LogInformation("DeleteCityCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("City deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

