using System;
namespace Contract.Responses
{
    public class AdjustmentDetailForListDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public byte[] RowVersion { get; init; } = [];

    }
}


