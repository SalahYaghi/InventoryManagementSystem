using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Product.DTOs;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Product.Queries.GetProductPaged
{
    public sealed class GetProductPagedQueryHandler : IRequestHandler<GetProductPagedQuery, Result<PaginatedList<ProductDtoForList>>>
    {
        private readonly ILogger<GetProductPagedQueryHandler> _logger;
        private readonly IAppDbContext _context;

        public GetProductPagedQueryHandler(IAppDbContext context, ILogger<GetProductPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<ProductDtoForList>>> Handle(
            GetProductPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetProductPagedQueryHandler));

            IQueryable<ProductDtoForList> result;


            if (request.ExcludeSupplierId.HasValue)
            {
                var productsToOmit = _context.SupplierProducts
                    .Where(s => s.SupplierId == request.ExcludeSupplierId.Value)
                    .Select(s => s.ProductId);

                result = _context.Products
                    .Where(p => p.IsActive && !productsToOmit.Contains(p.Id))
                    .OrderBy(p => p.ProductName)
                    .Select(entity => new ProductDtoForList
                    {
                        Id = entity.Id,
                        SKU = entity.SKU,
                        BarCode = entity.BarCode,
                        ProductName = entity.ProductName,
                        SellingPrice = entity.SellingPrice,
                        IsActive = entity.IsActive,
                        Unit = entity.Unit.ToString(),
                        Category = entity.Category!.Name
                    });
            }
            else if (request.fromWarehouseId.HasValue)
            {
                result = _context.WarehouseStocks
                    .Where(w => w.WarehouseId == request.fromWarehouseId && w.Product!.IsActive)
                    .Select(e => new
                    {
                        entity = e,
                        ReservedQuantity =
                            _context.OrderDetails
                                .Where(w => w.ProductId == e.ProductId &&
                                            w.Order!.OrderStatus == Domain.Orders.OrderStatus.Pending &&
                                            w.Order!.SourceWarehouseId == request.fromWarehouseId &&
                                            (w.Order!.OrderType == Domain.Orders.OrderType.Sale ||
                                             w.Order!.OrderType == Domain.Orders.OrderType.Transfer ||
                                             w.Order!.OrderType == Domain.Orders.OrderType.ReturnOut))
                                .Sum(v => v.Quantity)
                            + _context.AdjustmentDetails
                                .Where(w => w.ProductId == e.ProductId &&
                                            w.Adjustment!.WarehouseId == request.fromWarehouseId &&
                                            w.Adjustment!.AdjustmentStatus == Domain.Adjustments.AdjustmentStatus.Draft &&
                                            w.Adjustment.AdjustmentType == Domain.Adjustments.AdjustmentType.Decrease)
                                .Sum(v => v.Quantity)
                    })
                    .OrderBy(x => x.entity.Product!.ProductName)
                    .Select(x => new ProductDtoForList
                    {
                        Id = x.entity.Product!.Id,
                        SKU = x.entity.Product!.SKU,
                        BarCode = x.entity.Product!.BarCode,
                        ProductName = x.entity.Product!.ProductName,
                        SellingPrice = x.entity.Product!.SellingPrice,
                        IsActive = x.entity.Product!.IsActive,
                        Unit = x.entity.Product!.Unit.ToString(),
                        Category = x.entity.Product!.Category!.Name,
                        Quantity = x.entity.Quantity,
                        RowVersion = x.entity.RowVersion,
                        ReservedQuantity = x.ReservedQuantity,
                        TotalQuantity = x.entity.Quantity - x.ReservedQuantity
                    });
            }
            else if (request.fromSupplierId.HasValue)
            {
                result = _context.SupplierProducts
                    .Where(s => s.SupplierId == request.fromSupplierId && s.Product!.IsActive)
                    .OrderBy(s => s.Product!.ProductName)
                    .Select(entity => new ProductDtoForList
                    {
                        Id = entity.Product!.Id,
                        SKU = entity.Product!.SKU,
                        BarCode = entity.Product!.BarCode,
                        ProductName = entity.Product!.ProductName,
                        SellingPrice = entity.Product!.SellingPrice,
                        IsActive = entity.Product!.IsActive,
                        Unit = entity.Product!.Unit.ToString(),
                        Category = entity.Product!.Category!.Name,
                        PurchasePrice = entity.PurchasePrice,
                        RowVersion = entity.RowVersion
                    });
            }
            else
            {
                result = _context.Products
                    .Where(p => p.IsActive)
                    .OrderBy(p => p.ProductName)
                    .Select(entity => new ProductDtoForList
                    {
                        Id = entity.Id,
                        SKU = entity.SKU,
                        BarCode = entity.BarCode,
                        ProductName = entity.ProductName,
                        SellingPrice = entity.SellingPrice,
                        IsActive = entity.IsActive,
                        Unit = entity.Unit.ToString(),
                        Category = entity.Category!.Name
                    });
            }

            if (request.excludeProductsIds is { Count: > 0 })
            {
                result = result.Where(p => !request.excludeProductsIds.Contains(p.Id));
            }

            var finalList = await result
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            _logger.LogInformation("GetProductPagedQueryHandler completed successfully.");
            return finalList;
        }
    }
}
