using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.References.ContactInfos.DTOs;
using Contract.Features.References.ContactInfos.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.ContactInfos.Commands.UpdateContactInfo
{
    public sealed class UpdateContactInfoCommandHandler : IRequestHandler<UpdateContactInfoCommand, Result<ContactInfoDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateContactInfoCommandHandler> _logger;

        public UpdateContactInfoCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateContactInfoCommandHandler> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<ContactInfoDto>> Handle(UpdateContactInfoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateContactInfoCommandHandler));

            var entity = await _context.ContactInfos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateContactInfoCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"ContactInfo.NotFound\", \"ContactInfo was not found.\")");
                return Error.NotFound("ContactInfo.NotFound", "ContactInfo was not found.");

            }

            entity.Update(request.Email, request.PhoneNumber, request.AlternitavePhoneNumber, request.FaxNumber, request.WebsiteUrl);

            _logger.LogInformation("UpdateContactInfoCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateContactInfoCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.ContactInfo), cancellationToken);

            _logger.LogInformation("ContactInfo updated successfully with key {Key}", request.Id);

            return entity.ToDto();
        }
    }
}

