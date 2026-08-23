using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Transactions.Order.DTOs;

namespace Contract.Features.Transactions.Order.Commands.UpdateOrderDetail
{
    public sealed record UpdateOrderDetailCommand : IRequest<Result<Updated>>
    {
        public Guid Id { get; init; }
        public decimal Quantity { get; init; }
        public byte[] RowVersion { get; init; } = [];
    }
}

