using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Inventory.Product.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(Guid Id) : IRequest<Result<Deleted>>;
}

