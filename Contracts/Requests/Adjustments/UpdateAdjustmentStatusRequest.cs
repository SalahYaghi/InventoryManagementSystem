using Contract.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Adjustment
{
    public class UpdateAdjustmentStatusRequest
    {
            public Guid Id { get; set; }
            public AdjustmentStatus AdjustmentStatus { get; set; }
        
    }
}


