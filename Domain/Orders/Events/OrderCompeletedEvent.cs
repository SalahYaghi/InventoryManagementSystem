using Inventory.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Orders.Events
{
    public class OrderCompeletedEvent : DomainEvent
    {
        public Guid OrderId { get; set; } 
    }
}
