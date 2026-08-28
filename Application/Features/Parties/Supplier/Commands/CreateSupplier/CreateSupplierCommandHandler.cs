using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.Parties.Supplier.Mappers;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Domain.Suppliers;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Supplier.Commands.CreateSupplier
{
    public sealed class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Result<SupplierDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateSupplierCommandHandler> _logger;

        public CreateSupplierCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateSupplierCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<SupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateSupplierCommandHandler));

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

                    _logger.LogError("CreateSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "contactInfoResult.Errors");
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

                    _logger.LogError("CreateSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }


            // validate supplier unique SKU
            if (await _context.Suppliers.AnyAsync(s => s.SupplierCode == request.SupplierCode)) {
                return SupplierErrors.DuplicateSupplierCode; 
            }


            var entityResult = Domain.Suppliers.Supplier.Create(request.Id, request.SupplierName, request.SupplierCode, contactInfo, address, request.Status, request.Notes);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateSupplierCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            _context.Suppliers.Add(entityResult.Value);
            _logger.LogInformation("CreateSupplierCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateSupplierCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Supplier), cancellationToken);

            _logger.LogInformation("Supplier created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

