using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Categories.DTOs;

namespace Contract.Features.Inventory.Categories.Queries.GetCategory
{
    public sealed record GetCategoryQuery(Guid Id) : ICachedQuery<Result<CategoryDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Inventory, CacheEntities.Category, nameof(GetCategoryQuery), Id);
        public string[] Tags => [CacheEntities.Category];
    }
}

