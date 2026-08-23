using Contract.Common.Interfaces;
using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.Addresses.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Addresses.Queries.GetAddress
{
    public sealed class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, Result<AddressDto>>
    {
        private readonly ILogger<GetAddressQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetAddressQueryHandler(IAppDbContext context,
            ILogger<GetAddressQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<AddressDto>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetAddressQueryHandler));

            var entity = await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetAddressQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Address.NotFound\", \"Address was not found.\")");
                return Error.NotFound("Address.NotFound", "Address was not found.");

            }

            _logger.LogInformation("GetAddressQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

