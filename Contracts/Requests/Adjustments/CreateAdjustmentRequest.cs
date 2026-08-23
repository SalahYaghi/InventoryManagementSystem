using Contract.Common;
using Contract.Requests.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Adjustments
{
    public class CreateAdjustmentRequest
    {
        public List<CreateAdjustmentDetailRequestInner> AdjustmentDetails { get; set; } = new List<CreateAdjustmentDetailRequestInner>();
        public AdjustmentType? AdjustmentType { get; set; }
        public AdjustmentReason AdjustmentReason { get; set; }

        public Guid WarehouseId { get; set; }
        public string Notes { get; set; }
      }
}


