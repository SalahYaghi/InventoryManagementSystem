using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;

namespace Contract.Features.Transactions.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand : IRequest<Result<OrderDto>>
    {
        public List<CreateOrderDetailCommand> OrderDetails { get; set; } = new();
        public Domain.Orders.OrderType OrderType { get; init; }
        public Guid? SupplierId { get; init; }
        public Guid? CustomerId { get; init; }
        public Guid SourceWarehouseId { get; init; }
        public Guid? DestinationWarehouseId { get; init; }
        public string? Notes { get; init; }
        public DateTimeOffset DueDate { get; init; }
        public decimal ? Discount { get; init; }
    }
}

