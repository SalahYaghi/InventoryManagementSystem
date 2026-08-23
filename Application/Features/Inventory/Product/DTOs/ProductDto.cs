using Contract.Features.Inventory.Categories.DTOs;
using Domain.Products.Category;

namespace Contract.Features.Inventory.Product.DTOs
{
    public sealed record ProductDto
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
        public CategoryDto? Category    { get; init; }
 
    }
}

