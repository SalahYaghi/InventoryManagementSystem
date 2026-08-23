using Contract.Common.Interfaces;
using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.Parties.Supplier.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Supplier.Queries.GetSupplier
{
    public sealed class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, Result<SupplierDto>>
    {
        private readonly ILogger<GetSupplierQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetSupplierQueryHandler(IAppDbContext context,
            ILogger<GetSupplierQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<SupplierDto>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetSupplierQueryHandler));

            var entity = await _context.Suppliers
                .Include(s => s.Contact)
                .Include(x => x.Address)
                .ThenInclude(a => a!.Country)
                .Include(x => x.Address)
                .ThenInclude(a => a!.City)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetSupplierQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Supplier.NotFound\", \"Supplier was not found.\")");
                return Error.NotFound("Supplier.NotFound", "Supplier was not found.");

            }

            var e =  entity.ToDto();
            _logger.LogInformation("GetSupplierQueryHandler completed successfully.");
            return e;
        }
    }
}

