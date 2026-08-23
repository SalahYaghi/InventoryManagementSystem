using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.References.Countries.DTOs;
using Contract.Features.References.Countries.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.Countries.Commands.UpdateCountry
{
    public sealed class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Result<CountryDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateCountryCommandHandler> _logger;

        public UpdateCountryCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateCountryCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CountryDto>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateCountryCommandHandler));

            var entity = await _context.Countries.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateCountryCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Country.NotFound\", \"Country was not found.\")");
                return Error.NotFound("Country.NotFound", "Country was not found.");

            }

            entity.Update(request.Name);

            _logger.LogInformation("UpdateCountryCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateCountryCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Country), cancellationToken);

            _logger.LogInformation("Country updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

