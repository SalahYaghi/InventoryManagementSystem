using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Orders.DTOs;
using Domain.Orders;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderStatusCommand : IRequest<Result<OrderDto>>
    {
        public Guid Id { get; init; }
        public OrderStatus OrderStatus { get; init;  }
    }
}

