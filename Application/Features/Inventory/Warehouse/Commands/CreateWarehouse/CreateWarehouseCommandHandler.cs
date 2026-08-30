using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Inventory.Warehouses.Mappers;
using Domain.Contacts.Address;
using Domain.Warehouses;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Warehouses.Commands.CreateWarehouse
{
    public sealed class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Result<WarehouseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateWarehouseCommandHandler> _logger;

        public CreateWarehouseCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateWarehouseCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<WarehouseDto>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateWarehouseCommandHandler));

            Address? address = null;


            if (request.Address is not null)
            {
                Result<Domain.Contacts.Address.Address> addressResult
                    = Domain.Contacts.Address.Address.
                    Create(Guid.NewGuid(),
                    request.Address.CountryId,
                    request.Address.CityId,
                    request.Address.PostalCode,
                    request.Address.BuildingNumber,
                    request.Address.Street,
                    request.Address.Description);

                if (addressResult.IsError)

                {

                    _logger.LogError("CreateWarehouseCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }
            var entityResult = Domain.Warehouses.Warehouse.Create(Guid.NewGuid(), request.Name, request.Code, address);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateWarehouseCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }
 

            _context.Warehouses.Add(entityResult.Value);
            _logger.LogInformation("CreateWarehouseCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateWarehouseCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Warehouse), cancellationToken);

            _logger.LogInformation("Warehouse created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

