using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Contacts.Address;
using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.Addresses.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.Addresses.Commands.CreateAddress
{
    public sealed class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result<AddressDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateAddressCommandHandler> _logger;

        public CreateAddressCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateAddressCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<AddressDto>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateAddressCommandHandler));

            var entityResult = Address.Create(Guid.NewGuid(), request.CountryId, request.CityId, request.PostalCode, request.BuildingNumber, request.Street, request.Description);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateAddressCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            _context.Addresses.Add(entityResult.Value);
            _logger.LogInformation("CreateAddressCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateAddressCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Address), cancellationToken);

            _logger.LogInformation("Address created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

