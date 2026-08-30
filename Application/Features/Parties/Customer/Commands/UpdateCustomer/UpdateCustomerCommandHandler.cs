using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.Parties.Customers.Mappers;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Customers.Commands.UpdateCustomer
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<CustomerDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateCustomerCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateCustomerCommandHandler));

            var entity = await _context.Customers
                .Include(c => c.Address)
                .Include(c => c.Contact)
             
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Customer.NotFound\", \"Customer was not found.\")");
                return Error.NotFound("Customer.NotFound", "Customer was not found.");

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

                    _logger.LogError("UpdateCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "contactInfoResult.Errors");
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

                    _logger.LogError("UpdateCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }
             
            var result =  entity.Update(request.CustomerName, 
                request.CustomerCode, contactInfo, address 
                , request.Notes);

            if (result.IsError)

            {

                _logger.LogError("UpdateCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _logger.LogInformation("UpdateCustomerCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateCustomerCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Customer), cancellationToken);

            _logger.LogInformation("Customer updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

