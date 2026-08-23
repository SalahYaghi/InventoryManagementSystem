using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Inventory.Warehouses.Mappers;
using Domain.Contacts.Address;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Warehouses.Commands.UpdateWarehouse
{
    public sealed class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Result<WarehouseDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateWarehouseCommandHandler> _logger;

        public UpdateWarehouseCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateWarehouseCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<WarehouseDto>> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateWarehouseCommandHandler));

            var entity = await _context.Warehouses
                .Include(w => w.Address)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateWarehouseCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Warehouse.NotFound\", \"Warehouse was not found.\")");
                return Error.NotFound("Warehouse.NotFound", "Warehouse was not found.");

            }
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

                    _logger.LogError("UpdateWarehouseCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }

            var result = entity.Update(request.Name, request.Code, address, request.WarehouseStatus);

            if (result.IsError)

            {

                _logger.LogError("UpdateWarehouseCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }
       
            _logger.LogInformation("UpdateWarehouseCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateWarehouseCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Warehouse), cancellationToken);

            _logger.LogInformation("Warehouse updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

