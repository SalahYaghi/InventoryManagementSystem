using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Product.DTOs;

namespace Contract.Features.Inventory.Product.Commands.CreateProduct
{
    public sealed record CreateProductCommand : IRequest<Result<ProductDto>>
    {
        public string SKU { get; init; } = string.Empty;
        public string? BarCode { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string? Description { get; init; }
        public decimal SellingPrice { get; init; }
        public bool IsActive { get; init; }
        public Domain.Products.Enums.Unit Unit { get; init; }
        public Guid CategoryId { get; init; }
    }
}

