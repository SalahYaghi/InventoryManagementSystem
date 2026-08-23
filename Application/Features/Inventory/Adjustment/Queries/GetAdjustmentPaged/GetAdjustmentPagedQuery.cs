using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Inventory.Adjustments.DTOs;
using MechanicShop.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Inventory.Adjustments.Queries.GetAdjustmentPaged
{
    public sealed record GetAdjustmentPagedQuery : ICachedQuery<Result<PaginatedList<AdjustmentForListDto>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;

        public string CacheKey => CacheKeys.ForEntityPaged(CacheGroups.Inventory, CacheEntities.Adjustment, nameof(GetAdjustmentPagedQuery), PageNumber, PageSize);
        public string[] Tags => [CacheEntities.Adjustment];
    }
}

