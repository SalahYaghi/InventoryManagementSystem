using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Contacts.Address.Country;
using Contract.Features.References.Countries.DTOs;
using Contract.Features.References.Countries.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.Countries.Commands.CreateCountry
{
    public sealed class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Result<CountryDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateCountryCommandHandler> _logger;

        public CreateCountryCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateCountryCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CountryDto>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateCountryCommandHandler));

            var entityResult = Country.Create(request.Name);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateCountryCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            _context.Countries.Add(entityResult.Value);
            _logger.LogInformation("CreateCountryCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateCountryCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Country), cancellationToken);

            _logger.LogInformation("Country created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

