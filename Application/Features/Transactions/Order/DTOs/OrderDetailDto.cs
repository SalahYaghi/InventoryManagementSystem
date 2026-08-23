using Contract.Features.Inventory.Product.DTOs;
using Domain.Products;

namespace Contract.Features.Transactions.Order.DTOs
{
 
    public sealed record OrderDetailDto
    {
        public Guid Id { get; init; }
        public Guid? OrderId { get; init; }
        public Guid ProductId { get; init; }
        public ProductDto? Product { get; init; }
        public decimal Quantity { get; init; }
        public byte[] RowVersion { get; set; } = [];
        public decimal? ActualQuantity { get; init; }
        public decimal UnitPrice { get; init; }
    }
}

