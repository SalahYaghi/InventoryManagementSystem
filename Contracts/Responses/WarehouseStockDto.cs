using System;
namespace Contract.Responses
{
    public class WarehouseStockDto
    {
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal MinimumStockLevel { get; set; }
        public byte[] RowVersion { get; set; } = new byte[0];
    }
}


