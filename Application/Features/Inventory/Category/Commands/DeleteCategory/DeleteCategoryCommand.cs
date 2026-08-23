using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Inventory.Categories.Commands.DeleteCategory
{
    public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Result<Deleted>>;
}

