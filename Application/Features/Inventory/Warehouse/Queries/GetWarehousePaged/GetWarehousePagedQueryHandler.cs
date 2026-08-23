using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Warehouse.DTOs;
using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Inventory.Warehouses.Mappers;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Warehouses.Queries.GetWarehousePaged
{
    public sealed class GetWarehousePagedQueryHandler : IRequestHandler<GetWarehousesQuery, Result<List<WarehouseForListDto>>>
    {
        private readonly ILogger<GetWarehousePagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetWarehousePagedQueryHandler(IAppDbContext context,
            ILogger<GetWarehousePagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<WarehouseForListDto>>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetWarehousePagedQueryHandler));

            var query = await _context.Warehouses
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(entity => new WarehouseForListDto() {
                    Id = entity.Id,
                    Name = entity.Name,
                    Code = entity.Code,
                    IsActived = entity.WarehouseStatus == Domain.Warehouses.WarehouseStatus.Active,
                    BuildingNumber = entity.Address!.BuildingNumber ?? string.Empty,
                    Street = entity.Address!.Street ?? string.Empty,
                })
                .ToListAsync(cancellationToken);

         
            _logger.LogInformation("GetWarehousePagedQueryHandler completed successfully.");
            return query;
        }
    }
}

