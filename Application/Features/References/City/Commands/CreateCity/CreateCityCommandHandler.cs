using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Contacts.Address.Country;
using Contract.Features.References.Cities.DTOs;
using Contract.Features.References.Cities.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.Cities.Commands.CreateCity
{
    public sealed class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Result<CityDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateCityCommandHandler> _logger;

        public CreateCityCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateCityCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CityDto>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateCityCommandHandler));

            var entityResult = City.Create(request.Id,request.CountryId, request.Name);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateCityCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            _context.Cities.Add(entityResult.Value);
            _logger.LogInformation("CreateCityCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateCityCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.City), cancellationToken);

            _logger.LogInformation("City created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

