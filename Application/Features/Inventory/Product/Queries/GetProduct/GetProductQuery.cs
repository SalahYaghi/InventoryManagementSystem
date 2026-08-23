using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Product.DTOs;

namespace Contract.Features.Inventory.Product.Queries.GetProduct
{
    public sealed record GetProductQuery(Guid Id) : ICachedQuery<Result<ProductDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Inventory, CacheEntities.Product, nameof(GetProductQuery), Id);
        public string[] Tags => [CacheEntities.Product];
    }
}

