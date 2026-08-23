using Contract.Features.Inventory.Product.DTOs;

namespace Contract.Features.Inventory.Adjustment.DTOs
{
    public sealed record AdjustmentDetailDto
    {
        public Guid Id { get; init; }
        public Guid AdjustmentId { get; init; }
        public Guid ProductId { get; init; }
        public ProductDto? Product { get; init;  }
        public decimal Quantity { get; init; }
        public byte[] RowVersion { get; init; } = [];
    }
}

