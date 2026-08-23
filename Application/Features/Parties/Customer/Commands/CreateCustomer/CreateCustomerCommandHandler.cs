using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.Parties.Customers.Mappers;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Domain.Customer;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Contract.Common.Errors;

namespace Contract.Features.Parties.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateCustomerCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateCustomerCommandHandler));

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

                    _logger.LogError("CreateCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "contactInfoResult.Errors");
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

                    _logger.LogError("CreateCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }
           
            var codeTaken = await _context.Customers
                .AnyAsync(c => c.CustomerCode == request.CustomerCode, cancellationToken);

            if (codeTaken)
            {
                _logger.LogWarning("CreateCustomerCommandHandler stopped: customer code {Code} already exists.", request.CustomerCode);
                return ApplicationErrors.CustomerCodeAlreadyExists;
            }

            var entityResult = Customer.Create( Guid.NewGuid(), 
                request.CustomerName, request.CustomerCode, contactInfo, address, request.Notes);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateCustomerCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }
            _logger.LogInformation("CreateCustomerCommandHandler is adding new entity data to the context.");
            await _context.Customers.AddAsync(entityResult.Value, cancellationToken);
            _logger.LogInformation("CreateCustomerCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateCustomerCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Customer), cancellationToken);

            _logger.LogInformation("Customer created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

