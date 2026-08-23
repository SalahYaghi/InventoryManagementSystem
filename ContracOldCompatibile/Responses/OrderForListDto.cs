using System;
namespace Contract.Responses
{
    public class OrderForListDto
    {
        public Guid Id { get; set; }
        public string OrderType { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public Guid? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid SourceWarehouseId { get; set; }
        public string SourceWarehouseName { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public string DestinationWarehouseName { get; set; }
        public DateTime DueDate { get; set; } 
        public decimal DiscountAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal NetAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}



