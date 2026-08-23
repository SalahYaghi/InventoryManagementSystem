using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.Adjustment.DTOs
{
    public sealed record AdjustmentForListDto
    {
        public Guid Id { get; init; }
        public string AsjustmentType { get; init; } = string.Empty;
        public string AdjustmentStatus { get; init; } = string.Empty;
        public string AdjustmentReason { get; init; } = string.Empty;

        public Guid? WarehouseId { get; init; }
        public string WarehouseName { get; init; } = string.Empty;

        public DateTimeOffset? AprovedAt { get; init; }
        public DateTimeOffset CreatedAt { get; init; }

    }
    public sealed record AdjustmentDetailForListDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public decimal Quantity { get; init; }
        public byte[] RowVersion { get; init; } = [];

    }
}

