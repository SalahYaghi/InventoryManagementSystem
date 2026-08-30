using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;

namespace Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail
{
    public sealed record CreateOrderDetailCommand : IRequest<Result<OrderDetailDto>>
    {
          public Guid ProductId { get; init; }
        public decimal Quantity { get; init; }
         public byte[] RowVersion { get; set; } = [];
    }
}

