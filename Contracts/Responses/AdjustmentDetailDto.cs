using System;
namespace Contract.Responses
{
    public class AdjustmentDetailDto
    {
        public Guid Id { get; set; }
        public ProductDto Product { get; set; }
        public Guid AdjustmentId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public byte[] RowVersion { get; init; } = [];

    }
}


