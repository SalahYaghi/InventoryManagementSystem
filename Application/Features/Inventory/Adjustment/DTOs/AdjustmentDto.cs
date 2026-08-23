using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Inventory.Warehouses.DTOs;

namespace Contract.Features.Inventory.Adjustments.DTOs
{
    public sealed record AdjustmentDto
    {
        public Guid Id { get; init; }
        public Guid WarehouseId { get; init; }
        public Domain.Adjustments.AdjustmentType AdjustmentType { get; init; }
        public Domain.Adjustments.AdjustmentReason AdjustmentReason { get; init; }
        public Domain.Adjustments.AdjustmentStatus AdjustmentStatus { get; init; }
        public string? Notes { get; init; }
        public WarehouseDto? Warehouse { get; set; }
        public List<AdjustmentDetailDto> AdjustmentDetailDtos { get; init; } = new();
    }
}

