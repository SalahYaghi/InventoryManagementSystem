using System;

namespace Contract.Responses
{
    public sealed class AdjustmentDetailDto
    {
        public Guid Id { get; set; }
        public Guid AdjustmentId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public byte[] RowVersion { get; set; }

        public string ProductName => Product.ProductName; 
        public decimal Quantity { get; set; }
    }
}



