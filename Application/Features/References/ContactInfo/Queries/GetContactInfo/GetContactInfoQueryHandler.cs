using Contract.Common.Interfaces;
using Contract.Features.References.ContactInfos.DTOs;
using Contract.Features.References.ContactInfos.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.ContactInfos.Queries.GetContactInfo
{
    public sealed class GetContactInfoQueryHandler : IRequestHandler<GetContactInfoQuery, Result<ContactInfoDto>>
    {
        private readonly ILogger<GetContactInfoQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetContactInfoQueryHandler(IAppDbContext context,
            ILogger<GetContactInfoQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<ContactInfoDto>> Handle(GetContactInfoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetContactInfoQueryHandler));

            var entity = await _context.ContactInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetContactInfoQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"ContactInfo.NotFound\", \"ContactInfo was not found.\")");
                return Error.NotFound("ContactInfo.NotFound", "ContactInfo was not found.");

            }

            _logger.LogInformation("GetContactInfoQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

