using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Parties.SupplierProducts.Commands.DeleteSupplierProduct
{
    public sealed record DeleteSupplierProductCommand(Guid SupplierId , 
        Guid ProductId) : IRequest<Result<Deleted>>;
}

