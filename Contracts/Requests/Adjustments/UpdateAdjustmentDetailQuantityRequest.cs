using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Orders
{
    public class UpdateAdjustmentDetailQuantityRequest
    {
           
            public decimal Quantity { get; set; }
        public byte[] RowVersion { get; init; } = [];

    }
}


