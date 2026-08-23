using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.Product.DTOs
{
    public sealed record ProductDtoForList
    {
            public Guid Id { get; init; }
            public string SKU { get; init; } = string.Empty;
            public string? BarCode { get; init; }
            public string ProductName { get; init; } = string.Empty;
            public decimal SellingPrice { get; init; }
            public bool IsActive { get; init; }
            public string Unit { get; init; } = string.Empty;
            public string? Category { get; init; } = string.Empty;

        public byte[] RowVersion { get; init; } = [];

        public decimal? Quantity { get; init; }
        public decimal? ReservedQuantity { get; init;  }
        public decimal? TotalQuantity { get; init; }

            public decimal? PurchasePrice { get; init; }
         
    }
}

