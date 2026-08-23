using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Categories.DTOs;

namespace Contract.Features.Inventory.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand : IRequest<Result<CategoryDto>>
    {
         public string Name { get; init; } = string.Empty;
    }
}

