using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Product.DTOs;

namespace Contract.Features.Inventory.Product.Queries.GetProductPaged
{
    public sealed record GetProductPagedQuery : ICachedQuery<Result<PaginatedList<ProductDtoForList>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;
        public Guid? ExcludeSupplierId { get;init; }
        
       public List<Guid>? excludeProductsIds { get; init;  }

       public Guid? fromWarehouseId { get; init;}
       public Guid? fromSupplierId { get; init;}

        public string CacheKey => CacheKeys.ForEntityPaged(
    CacheGroups.Inventory,
    CacheEntities.Product,
    $"{nameof(GetProductPagedQuery)}:" +
    $"excludeSupplier={ExcludeSupplierId}:" +
    $"warehouse={fromWarehouseId}:" +
    $"supplier={fromSupplierId}:" +
    $"excludedProducts={GetExcludedProductsKey()}",
    PageNumber,
    PageSize);

        private object GetExcludedProductsKey()
        {
            if (excludeProductsIds == null || excludeProductsIds.Count == 0)
                return "none";

            return string.Join(",", excludeProductsIds.OrderBy(x => x));
        }

        public string[] Tags => [CacheEntities.Product];
    }
}

