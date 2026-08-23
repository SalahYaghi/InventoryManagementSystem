using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Product.DTOs;

namespace Contract.Features.Inventory.Product.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand : IRequest<Result<ProductDto>>
    {
        public Guid Id { get; init; }
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

