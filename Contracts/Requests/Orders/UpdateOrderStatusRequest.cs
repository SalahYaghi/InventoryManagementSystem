using Contract.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Orders
{
    public class UpdateOrderStatusRequest
    {
            public Guid Id { get; set; }
            public OrderStatus OrderStatus { get; set; }
        
    }
}


