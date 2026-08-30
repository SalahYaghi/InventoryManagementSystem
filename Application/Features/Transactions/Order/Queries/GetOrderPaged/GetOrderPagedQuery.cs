using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Transactions.Order.DTOs;
using Contract.Features.Transactions.Orders.DTOs;
using Domain.Orders;
using Inventory.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Transactions.Orders.Queries.GetOrderPaged
{
    public sealed record GetOrderPagedQuery : ICachedQuery<Result<PaginatedList<OrderForListDto>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;

        public OrderType? OrderType { get; init;  }

        public string CacheKey => CacheKeys.ForEntityPaged(CacheGroups.Transactions, CacheEntities.Order, nameof(GetOrderPagedQuery) + OrderType.ToString(), PageNumber, PageSize);
        public string[] Tags => [CacheEntities.Order];
    }
}

