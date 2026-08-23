using System;
using System.Collections.Generic;

namespace Contract.Responses
{
    public class OrderDto
    {
        public Guid? Id { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();

        public Guid? SupplierId { get; set; }
        public SupplierDto Supplier { get; set; }

        public Guid? CustomerId { get; set; }
        public CustomerDto Customer { get; set; }

        public Guid? InvoiceId { get; set; }
        public InvoiceDto Invoice { get; set; }

        public Guid? SourceWarehouseId { get; set; }
        public WarehouseDto SourceWarehouseDto { get; set; }

        public DateTime DueDate { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public WarehouseDto DestinationWarehouseDto { get; set; }

        public decimal NetAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string Notes { get; set; }
    }

}



