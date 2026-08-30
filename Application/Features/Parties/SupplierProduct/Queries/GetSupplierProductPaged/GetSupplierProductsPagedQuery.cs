using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Parties.SupplierProduct.DTOs;
using Contract.Features.Parties.SupplierProducts.DTOs;
using Inventory.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Parties.SupplierProducts.Queries.GetSupplierProductPaged
{
    public sealed record GetSupplierProductsPagedQuery(Guid SupplierId) : ICachedQuery<Result<List<SupplierProductDtoForList>>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Parties, CacheEntities.SupplierProduct, nameof(GetSupplierProductsPagedQuery) , SupplierId);
        public string[] Tags => [CacheEntities.SupplierProduct];
    }
}

