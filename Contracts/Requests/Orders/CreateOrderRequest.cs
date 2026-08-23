using Contract.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Orders
{
    public class CreateOrderRequest
    {
        public List<CreateOrderDetailRequestInner> OrderDetails { get; set; } = new List<CreateOrderDetailRequestInner>();
        public OrderType OrderType { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid SourceWarehouseId { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public string ?Notes { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public decimal Discount { get; set; }
    }
}


