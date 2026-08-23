using Contract.Common.Interfaces;
using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Inventory.Warehouses.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Warehouses.Queries.GetWarehouse
{
    public sealed class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, Result<WarehouseDto>>
    {
        private readonly ILogger<GetWarehouseQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetWarehouseQueryHandler(IAppDbContext context,
            ILogger<GetWarehouseQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<WarehouseDto>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetWarehouseQueryHandler));

            var entity = await _context.Warehouses
                .Include(w => w.Address)
                    .ThenInclude(a => a!.Country)
                .Include(w => w.Address)
                    .ThenInclude(a => a!.City)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetWarehouseQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Warehouse.NotFound\", \"Warehouse was not found.\")");
                return Error.NotFound("Warehouse.NotFound", "Warehouse was not found.");

            }

            _logger.LogInformation("GetWarehouseQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

