using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Orders.DTOs;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand : IRequest<Result<OrderDto>>
    {
        public Guid Id { get; init; }
        public decimal DiscountAmount { get; init; }
        public string? Notes { get; init; }
        public DateTimeOffset? DueDate { get; init; }
    }
}

