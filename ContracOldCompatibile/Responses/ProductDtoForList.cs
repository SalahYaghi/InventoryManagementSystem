using System;

namespace Contract.Responses
{
    public sealed class ProductDtoForList
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string BarCode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public byte[] RowVersion { get; set; }// = [];
        public decimal? Quantity { get; set; }
        public decimal? ReservedQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
         
        public decimal? PurchasePrice { get; set; }
      
    }
}



