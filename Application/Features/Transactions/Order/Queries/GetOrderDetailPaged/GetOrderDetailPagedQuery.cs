using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;

namespace Contract.Features.Transactions.Order.Queries.GetOrderDetailPaged
{
    public sealed record GetOrderDetailPagedQuery(Guid OrderId) : ICachedQuery<Result<List<OrderDetailForListDto>>>
    {
        public string CacheKey => CacheKeys.ForEntityList(
            CacheGroups.Transactions, CacheEntities.OrderDetail, nameof(GetOrderDetailPagedQuery), OrderId);
        public string[] Tags => [CacheEntities.OrderDetail];
    }
}

