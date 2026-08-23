using Contract.Common.Constants;

using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Parties.SupplierProducts.DTOs;

namespace Contract.Features.Parties.SupplierProducts.Queries.GetSupplierProduct
{
    public sealed record GetSupplierProductQuery(Guid SupplierId , Guid ProductId) : ICachedQuery<Result<SupplierProductDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Parties, CacheEntities.SupplierProduct, nameof(GetSupplierProductQuery), SupplierId);
        public string[] Tags => [CacheEntities.SupplierProduct];
    }
}

