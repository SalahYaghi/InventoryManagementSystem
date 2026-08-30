using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Product.Mappers;
using Contract.Features.Inventory.WarehouseStock.DTOs;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Contract.Features.Inventory.WarehouseStocks.Mappers;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged
{
    public sealed class GetWarehouseStockPagedQueryHandler : IRequestHandler<GetWarehouseStockPagedQuery, Result<PaginatedList<WarehouseStockDtoForList>>>
    {
        private readonly ILogger<GetWarehouseStockPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetWarehouseStockPagedQueryHandler(IAppDbContext context,
            ILogger<GetWarehouseStockPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<WarehouseStockDtoForList>>> Handle(GetWarehouseStockPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetWarehouseStockPagedQueryHandler));

            var query = _context.WarehouseStocks
                .AsNoTracking()
                .Where(f => f.WarehouseId == request.WarehouseId)
                 .Select(e => new {
                     entity = e,
                     ReservedQuantity = _context.OrderDetails
                        .Where(w => w.ProductId == e.ProductId &&
                        w.Order!.OrderStatus == Domain.Orders.OrderStatus.Pending &&
                        w.Order!.OrderType != Domain.Orders.OrderType.Purchase)
                        .Sum(v => v.Quantity) + _context.AdjustmentDetails.Where(w => w.ProductId == e.ProductId &&
                        w.Adjustment!.AdjustmentStatus == Domain.Adjustments.AdjustmentStatus.Draft &&
                        w.Adjustment.AdjustmentType == Domain.Adjustments.AdjustmentType.Decrease).Sum(v => v.Quantity)
                 })
                 .Select(entity => new WarehouseStockDtoForList()
                 {
                     Id = entity.entity.Id,
                     Quantity = entity.entity.Quantity,
                     RowVersion = entity.entity.RowVersion,
                     MinimumStockLevel = entity.entity.MinimumStockLevel,
                     ProductId = entity.entity.Product!.Id,
                     SKU = entity.entity.Product!.SKU,
                     BarCode = entity.entity.Product!.BarCode,
                     ProductName = entity.entity.Product!.ProductName,
                     SellingPrice = entity.entity.Product!.SellingPrice,
                     IsActive = entity.entity.Product!.IsActive,
                     Unit = entity.entity.Product!.Unit.ToString(),
                     Category = entity.entity.Product!.Category!.Name,
                     ReservedQuantity = entity.ReservedQuantity,
                     TotalQuantity = entity.entity.Quantity - entity.ReservedQuantity

                 });

            var result = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("GetWarehouseStockPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

