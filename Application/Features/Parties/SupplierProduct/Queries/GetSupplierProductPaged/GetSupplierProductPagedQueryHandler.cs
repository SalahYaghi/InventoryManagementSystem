using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Inventory.Product.Mappers;
using Contract.Features.Parties.SupplierProduct.DTOs;
using Contract.Features.Parties.SupplierProducts.DTOs;
using Contract.Features.Parties.SupplierProducts.Mappers;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.SupplierProducts.Queries.GetSupplierProductPaged
{
    public sealed class GetSupplierProductPagedQueryHandler : IRequestHandler<GetSupplierProductsPagedQuery, Result<List<SupplierProductDtoForList>>>
    {
        private readonly ILogger<GetSupplierProductPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetSupplierProductPagedQueryHandler(IAppDbContext context,
            ILogger<GetSupplierProductPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<SupplierProductDtoForList>>> Handle(GetSupplierProductsPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetSupplierProductPagedQueryHandler));

            var query = await _context.SupplierProducts
                .Where(x => x.SupplierId == request.SupplierId)
                .AsNoTracking()
                .Select(x => new SupplierProductDtoForList() {
                    Id = x.Product!.Id,
                    ProductName = x.Product!.ProductName,
                    IsActive = x.Product!.IsActive,
                   ProductId = x.ProductId,
                   PurchasePrice = x.PurchasePrice,
                   SupplierId = x.SupplierId,
                   CreatedAt = x.CreatedAtUtc , 
                   UpdatedAt = x.LastModifiedUtc ,
                   RowVersion = x.RowVersion,
                })
                .ToListAsync(cancellationToken);  

            _logger.LogInformation("GetSupplierProductPagedQueryHandler completed successfully.");
            return query;
        }
    }
}

