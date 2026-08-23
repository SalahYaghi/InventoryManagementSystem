using System;
namespace Contract.Responses
{
    public class WarehouseStockDtoForList
    {
       
            public Guid Id { get; set; }
             public decimal MinimumStockLevel { get; set; }

            public Guid ProductId { get; init; }
            public string SKU { get; init; } = string.Empty;
            public string? BarCode { get; init; }
            public string ProductName { get; init; } = string.Empty;
            public decimal SellingPrice { get; init; }
            public bool IsActive { get; init; }
            public string Unit { get; init; } = string.Empty;
            public string? Category { get; init; } = string.Empty;
        public decimal Quantity { get; set; }

        public decimal? ReservedQuantity { get; init; }
        public decimal? TotalQuantity { get; init; }

        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }


        public byte[] RowVersion { get; init; } = [];

        
    }

} 

