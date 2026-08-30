using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Parties.People.DTOs;
using Contract.Features.Parties.People.Mappers;
using Domain.People;
using Domain.Document;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;
using Domain.Contacts.ContactInfo;
using System.Reflection.Metadata;
using Domain.Contacts.Address;
using Contract.Common.Errors;

namespace Contract.Features.Parties.People.Commands.CreatePerson
{
    public sealed class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<PersonDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreatePersonCommandHandler> _logger;

        public CreatePersonCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreatePersonCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<PersonDto>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreatePersonCommandHandler));


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

                    _logger.LogError("CreatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "contactInfoResult.Errors");
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

                    _logger.LogError("CreatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }


            var nationalNoDuplicated = await _context.People.AnyAsync(p => p.NationalNo == request.NationalNo);

            if (nationalNoDuplicated)

            {

                _logger.LogWarning("CreatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.NationalNoAlreadyExist");
                return ApplicationErrors.NationalNoAlreadyExist;

            }

            
            
            var result = Domain.People.Person.Create(
                Guid.NewGuid(),
                request.NationalNo,
                request.FirstName,
                request.SecondName,
                request.ThirdName,
                request.LastName,
                request.Gender,
                request.DateOfBirth,
                contactInfo,
                address
                );

            if (result.IsError)

            {

                _logger.LogError("CreatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                return result.Errors;

            }

            _context.People.Add(result.Value);
            _logger.LogInformation("CreatePersonCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreatePersonCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Person), cancellationToken);

            _logger.LogInformation("Person created successfully with key {Key}", result.Value.Id);

            return result.Value.ToDto();
        }
    }
}

