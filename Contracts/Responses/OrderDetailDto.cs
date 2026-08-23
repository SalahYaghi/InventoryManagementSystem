using System;
namespace Contract.Responses
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public ProductDto? Product { get; set; }

        public byte[] RowVersion { get; set; } = [];
        public decimal ActualQuantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}


