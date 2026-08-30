using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Transactions.Orders.Commands.DeleteOrder
{
    public sealed record DeleteOrderCommand(Guid Id) : IRequest<Result<Deleted>>;
}

