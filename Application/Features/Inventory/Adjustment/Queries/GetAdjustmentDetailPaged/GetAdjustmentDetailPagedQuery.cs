using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Adjustment.DTOs;

namespace Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetailPaged
{
    public sealed record GetAdjustmentDetailPagedQuery (Guid AdjustmentId): ICachedQuery<Result<List<AdjustmentDetailForListDto>>>
    {
        
        public string CacheKey => CacheKeys.ForEntityList(
            CacheGroups.Inventory, CacheEntities.AdjustmentDetail, nameof(GetAdjustmentDetailPagedQuery), AdjustmentId);
        public string[] Tags => [CacheEntities.AdjustmentDetail];
    }
}

