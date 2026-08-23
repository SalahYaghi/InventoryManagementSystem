using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Orders
{
    public class CreateAdjustmentDetailRequestInner
    {
        public byte[] RowVersion { get; set; } = new byte[0];

        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
    public class CreateAdjustmentDetailRequest
    {
        public Guid AdjustmentId { get; set; }
        public byte[] RowVersion { get; set; } = new byte[0];

        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}



