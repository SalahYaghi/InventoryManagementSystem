using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Domain.Contacts.ContactInfo;
using Contract.Features.References.ContactInfos.DTOs;
using Contract.Features.References.ContactInfos.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.ContactInfos.Commands.CreateContactInfo
{
    public sealed class CreateContactInfoCommandHandler : IRequestHandler<CreateContactInfoCommand, Result<ContactInfoDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreateContactInfoCommandHandler> _logger;

        public CreateContactInfoCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreateContactInfoCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<ContactInfoDto>> Handle(CreateContactInfoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreateContactInfoCommandHandler));

            var entityResult = ContactInfo.Create(Guid.NewGuid(), request.Email, request.PhoneNumber, request.AlternitavePhoneNumber, request.FaxNumber, request.WebsiteUrl);

            if (entityResult.IsError)

            {

                _logger.LogError("CreateContactInfoCommandHandler stopped because an error result was returned: {ErrorResult}.", "entityResult.Errors");
                return entityResult.Errors;

            }

            _context.ContactInfos.Add(entityResult.Value);
            _logger.LogInformation("CreateContactInfoCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreateContactInfoCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.ContactInfo), cancellationToken);

            _logger.LogInformation("ContactInfo created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

