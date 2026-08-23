using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Transactions.Order.Commands.DeleteOrderDetail
{
    public sealed record DeleteAdjustmentDetailCommand(Guid Id) : IRequest<Result<Deleted>>;
}

