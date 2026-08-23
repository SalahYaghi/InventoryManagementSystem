using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Adjustment.DTOs;

namespace Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetail
{
    public sealed record GetAdjustmentDetailQuery(Guid Id) : ICachedQuery<Result<AdjustmentDetailDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Inventory, CacheEntities.AdjustmentDetail, nameof(GetAdjustmentDetailQuery), Id);
        public string[] Tags => [CacheEntities.AdjustmentDetail];
    }
}

