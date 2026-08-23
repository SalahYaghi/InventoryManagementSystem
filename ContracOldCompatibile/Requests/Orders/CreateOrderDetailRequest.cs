using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Orders
{
    public class CreateOrderDetailRequestInner
    {
        public byte[] RowVersion { get; set; } = new byte[0];

        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
    public class CreateOrderDetailRequest
    {
        public Guid OrderId { get; set; }
        public byte[] RowVersion { get; set; } = new byte[0];

        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}



