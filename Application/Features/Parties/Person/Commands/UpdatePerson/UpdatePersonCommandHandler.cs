using Contract.Common.Constants;
using Contract.Common.Errors;
using Contract.Common.Interfaces;
using Contract.Features.Parties.People.DTOs;
using Contract.Features.Parties.People.Mappers;
using Domain.Adjustments;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.People.Commands.UpdatePerson
{
    public sealed class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Result<PersonDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdatePersonCommandHandler> _logger;

        public UpdatePersonCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdatePersonCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<PersonDto>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdatePersonCommandHandler));

            var entity = await _context.People
                .Include(p => p.Contact)
                .Include(p => p.Address)
              
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Person.NotFound\", \"Person was not found.\")");
                return Error.NotFound("Person.NotFound", "Person was not found.");

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

                    _logger.LogError("UpdatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "contactInfoResult.Errors");
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

                    _logger.LogError("UpdatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "addressResult.Errors");
                    return addressResult.Errors;

                }

                address = addressResult.Value;

            }

            var nationalNoDuplicated = await _context.People.AnyAsync(p => p.NationalNo == request.NationalNo
            && p.Id != request.Id, cancellationToken); // [FIX 6.11] +ct

            if (nationalNoDuplicated)

            {

                _logger.LogWarning("UpdatePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.NationalNoAlreadyExist");
                return ApplicationErrors.NationalNoAlreadyExist;

            }



            var updateResult = entity.Update(
                request.NationalNo,
                request.FirstName,
                request.SecondName,
                request.ThirdName,
                request.LastName,
                request.Gender,
                request.DateOfBirth,
                contactInfo,
                address);

            if (updateResult.IsError)
            {
                _logger.LogError("UpdatePersonCommandHandler stopped: {Errors}", updateResult.Errors);
                return updateResult.Errors;
            }

            _logger.LogInformation("UpdatePersonCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdatePersonCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Person), cancellationToken);

            _logger.LogInformation("Person updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

