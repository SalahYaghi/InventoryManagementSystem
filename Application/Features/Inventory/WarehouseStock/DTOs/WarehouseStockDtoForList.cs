using Contract.Features.Inventory.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.WarehouseStock.DTOs
{
    public record WarehouseStockDtoForList
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal? ReservedQuantity { get; init; }
        public decimal? TotalQuantity { get; init; }

        public decimal MinimumStockLevel { get; set; }

        public Guid ProductId { get; init; }
        public string SKU { get; init; } = string.Empty;
        public string? BarCode { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public decimal SellingPrice { get; init; }
        public bool IsActive { get; init; }
        public string Unit { get; init; } = string.Empty;
        public string? Category { get; init; } = string.Empty;

        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }

        public byte[] RowVersion { get; init; } = [];

    }
}

