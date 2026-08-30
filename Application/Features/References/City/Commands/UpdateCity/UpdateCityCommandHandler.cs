using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.References.Cities.DTOs;
using Contract.Features.References.Cities.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.Cities.Commands.UpdateCity
{
    public sealed class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Result<CityDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateCityCommandHandler> _logger;

        public UpdateCityCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateCityCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CityDto>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateCityCommandHandler));

            var entity = await _context.Cities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateCityCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"City.NotFound\", \"City was not found.\")");
                return Error.NotFound("City.NotFound", "City was not found.");

            }

            entity.Update(request.Name);

            _logger.LogInformation("UpdateCityCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateCityCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.City), cancellationToken);

            _logger.LogInformation("City updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

