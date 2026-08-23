using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.References.ContactInfos.DTOs;
using Contract.Features.References.ContactInfos.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.ContactInfos.Queries.GetContactInfoPaged
{
    public sealed class GetContactInfoPagedQueryHandler : IRequestHandler<GetContactInfoPagedQuery, Result<PaginatedList<ContactInfoDto>>>
    {
        private readonly ILogger<GetContactInfoPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetContactInfoPagedQueryHandler(IAppDbContext context,
            ILogger<GetContactInfoPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<ContactInfoDto>>> Handle(GetContactInfoPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetContactInfoPagedQueryHandler));

            var query = _context.ContactInfos
                .AsNoTracking()
                .OrderBy(x => x.Email)
                .Select(x => x.ToDto());

            var result = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("GetContactInfoPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

