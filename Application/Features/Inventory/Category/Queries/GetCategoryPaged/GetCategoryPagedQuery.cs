using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Categories.DTOs;

namespace Contract.Features.Inventory.Categories.Queries.GetCategoryPaged
{
    public sealed record GetCategoryPagedQuery : ICachedQuery<Result<List<CategoryDto>>>
    {
        public string CacheKey => CacheKeys.ForEntityList(CacheGroups.Inventory, CacheEntities.Category, nameof(GetCategoryPagedQuery));
        public string[] Tags => [CacheEntities.Category];
    }
}

