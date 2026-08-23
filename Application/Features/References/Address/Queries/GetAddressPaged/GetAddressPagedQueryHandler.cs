using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.Addresses.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Addresses.Queries.GetAddressPaged
{
    public sealed class GetAddressPagedQueryHandler : IRequestHandler<GetAddressPagedQuery, Result<PaginatedList<AddressDto>>>
    {
        private readonly ILogger<GetAddressPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetAddressPagedQueryHandler(IAppDbContext context,
            ILogger<GetAddressPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<AddressDto>>> Handle(GetAddressPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetAddressPagedQueryHandler));

            var query = _context.Addresses
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Select(x => x.ToDto());

            var result = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("GetAddressPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

