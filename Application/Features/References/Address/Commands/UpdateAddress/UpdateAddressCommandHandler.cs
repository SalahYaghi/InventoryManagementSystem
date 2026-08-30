using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.Addresses.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.Addresses.Commands.UpdateAddress
{
    public sealed class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Result<AddressDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateAddressCommandHandler> _logger;

        public UpdateAddressCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateAddressCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<AddressDto>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateAddressCommandHandler));

            var entity = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateAddressCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Address.NotFound\", \"Address was not found.\")");
                return Error.NotFound("Address.NotFound", "Address was not found.");

            }

            var updateResult = entity.Update(
                request.CountryId, request.CityId, request.PostalCode,
                request.BuildingNumber, request.Street, request.Description);

            if (updateResult.IsError)
            {
                _logger.LogError("UpdateAddressCommandHandler stopped: {Errors}", updateResult.Errors);
                return updateResult.Errors;
            }

            _logger.LogInformation("UpdateAddressCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateAddressCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Address), cancellationToken);

            _logger.LogInformation("Address updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

