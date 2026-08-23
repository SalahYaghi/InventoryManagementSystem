using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.Countries.Commands.DeleteCountry
{
    public sealed class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteCountryCommandHandler> _logger;

        public DeleteCountryCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteCountryCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<Deleted>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteCountryCommandHandler));

            var entity = await _context.Countries.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteCountryCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Country.NotFound\", \"Country was not found.\")");
                return Error.NotFound("Country.NotFound", "Country was not found.");

            }

            _logger.LogInformation("DeleteCountryCommandHandler is marking entity data for persistence operation.");
            _context.Countries.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteCountryCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Country), cancellationToken);
            _logger.LogInformation("DeleteCountryCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Country deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

