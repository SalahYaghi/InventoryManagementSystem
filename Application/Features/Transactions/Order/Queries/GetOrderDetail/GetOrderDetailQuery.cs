using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;

namespace Contract.Features.Transactions.Order.Queries.GetOrderDetail
{
    public sealed record GetOrderDetailQuery(Guid Id) : ICachedQuery<Result<OrderDetailDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Transactions, CacheEntities.OrderDetail, nameof(GetOrderDetailQuery), Id);
        public string[] Tags => [CacheEntities.OrderDetail];
    }
}

