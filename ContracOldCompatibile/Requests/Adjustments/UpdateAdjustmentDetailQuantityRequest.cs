using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Orders
{
    public class UpdateAdjustmentDetailQuantityRequest
    {
        public byte[] RowVersion { get; set; } 

        public decimal Quantity { get; set; }
        
    }
}



