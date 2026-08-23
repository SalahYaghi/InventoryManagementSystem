using System;
namespace Contract.Responses
{
    public class ProductDtoForList
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string BarCode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public byte[] RowVersion { get; init; } = [];
        public decimal? Quantity { get; init; }
         public decimal? ReservedQuantity { get; init; }
        public decimal? TotalQuantity { get; init; }

        public decimal? PurchasePrice { get; init; }
    }
}


