using System;

namespace Contract.Responses
{
    public class WarehouseStockDtoForList
    {

        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal SellingPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal? ReservedQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }

        public decimal MinimumStockLevel { get; set; }
        public Guid ProductId { get; set;  }
        public string BarCode { get; set; }
        public bool IsActive { get; set; }
     
    
        public byte[] RowVersion { get; set; } 
    }
}



