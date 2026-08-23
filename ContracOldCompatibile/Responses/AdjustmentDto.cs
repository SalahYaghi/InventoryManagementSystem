using System.Collections.Generic;
using System;
 
namespace Contract.Responses
{
    public class AdjustmentDto
    {
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public WarehouseDto Warehouse { get; set; }
        public AdjustmentType AdjustmentType { get; set; }
        public  AdjustmentReason AdjustmentReason { get; set; }
        public  AdjustmentStatus AdjustmentStatus { get; set; }
        public string Notes { get; set; }
        public List<AdjustmentDetailDto> AdjustmentDetailDtos { get; set; } = new List<AdjustmentDetailDto>();
    }
}



