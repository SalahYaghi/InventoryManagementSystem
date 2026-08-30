using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Categories.DTOs;

namespace Contract.Features.Inventory.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

