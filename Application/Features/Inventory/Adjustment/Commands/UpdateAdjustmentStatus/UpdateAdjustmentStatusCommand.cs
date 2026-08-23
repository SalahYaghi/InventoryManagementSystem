using Contract.Features.Inventory.Adjustments.DTOs;
using Contract.Features.Transactions.Orders.DTOs;
using Domain.Adjustments;
using Domain.Orders;
using MechanicShop.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed record UpdateAdjustmentStatusCommand : IRequest<Result<Updated>>
    {
        public Guid Id { get; init; }
        public AdjustmentStatus AdjustmentStatus { get; init;  }
    }
}

