using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Parties.Customers.Commands.DeleteCustomer
{
    public sealed record DeleteCustomerCommand(Guid Id) : IRequest<Result<Deleted>>;
}

