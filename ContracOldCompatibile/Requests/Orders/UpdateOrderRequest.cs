using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Orders
{
    public class UpdateOrderRequest
    {
        
          public Guid Id { get; set; }
            public decimal DiscountAmount { get; set; }
            public string Notes { get; set; }
        public DateTimeOffset DueDate { get; set; }
        
    }

}



