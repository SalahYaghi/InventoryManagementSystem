using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Inventory.Product.Commands.DeleteProduct
{
    public sealed record DeleteProductImageCommand(Guid Id) : IRequest<Result<Deleted>>;
}

