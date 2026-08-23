using System;
namespace Contract.Responses
{
    public class AdjustmentForListDto
    {
        public Guid Id { get; set; }
        public string AsjustmentType { get; set; } = string.Empty;
        public string AdjustmentStatus { get; set; } = string.Empty;
        public string AdjustmentReason { get; set; } = string.Empty;
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public DateTime? AprovedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}



