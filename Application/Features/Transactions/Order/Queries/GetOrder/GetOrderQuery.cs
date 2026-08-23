using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Orders.DTOs;

namespace Contract.Features.Transactions.Orders.Queries.GetOrder
{
    public sealed record GetOrderQuery(Guid Id) : ICachedQuery<Result<OrderDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Transactions, CacheEntities.Order, nameof(GetOrderQuery), Id);
        public string[] Tags => [CacheEntities.Order];
    }
}

