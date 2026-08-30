using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Transactions.Order.Commands.DeleteOrderDetail
{
    public sealed record DeleteOrderDetailCommand(Guid Id) : IRequest<Result<Deleted>>;
}

