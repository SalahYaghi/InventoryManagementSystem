using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Adjustments.DTOs;

namespace Contract.Features.Inventory.Adjustments.Queries.GetAdjustment
{
    public sealed record GetAdjustmentQuery(Guid Id) : ICachedQuery<Result<AdjustmentDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Inventory, CacheEntities.Adjustment, nameof(GetAdjustmentQuery), Id);
        public string[] Tags => [CacheEntities.Adjustment];
    }
}

