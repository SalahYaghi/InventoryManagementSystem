using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Parties.Supplier.Commands.DeleteSupplier
{
    public sealed record DeleteSupplierCommand(Guid Id) : IRequest<Result<Deleted>>;
}

