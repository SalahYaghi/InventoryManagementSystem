using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.Parties.Supplier.Mappers;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Supplier.Commands.UpdateSupplier
{
    public sealed class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Result<SupplierDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateSupplierCommandHandler> _logger;

        public UpdateSupplierCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateSupplierCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<SupplierDto>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateSupplierCommandHandler));

            var entity = await _context.Suppliers
                .Include(s => s.Contact)
                .Include(s => s.Address)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Supplier.NotFound\", \"Supplier was not found.\")");
                return Error.NotFound("Supplier.NotFound", "Supplier was not found.");

            }
            ContactInfo? contactInfo = null;
            Address? address = null;


            if (request.Contact is not null)
            {
                Result<ContactInfo> contactInfoResult = ContactInfo.Create(Guid.NewGuid(),
                    request.Contact.Email,
                    request.Contact.PhoneNumber,
                    request.Contact.AlternitavePhoneNumber,
                    request.Contact.FaxNumber,
                    request.Contact.WebsiteUrl);

                if (contactInfoResult.IsError)

                {

                    _logger.LogError("UpdateSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "contactInfoResult.Errors");
                    return contactInfoResult.Errors;

                }

                contactInfo = contactInfoResult.Value;
            }


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

                    _logger.LogError("UpdateSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }


           var result =  entity.Update(request.SupplierName, request.SupplierCode,contactInfo,address, request.Status, request.Notes);

            if (result.IsError)

            {

                _logger.LogError("UpdateSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _logger.LogInformation("UpdateSupplierCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateSupplierCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Supplier), cancellationToken);

            _logger.LogInformation("Supplier updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

