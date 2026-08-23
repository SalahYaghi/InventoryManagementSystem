using System.Linq;
using System.Collections.Generic;
using System;
namespace Contract.Responses
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string InvoiceType { get; set; } = string.Empty;
        public decimal NetAmount => SubTotalAmount + TaxAmount - DiscountAmount;
        public decimal SubTotalAmount => InvoiceLineItems.Sum(i => i.TotalAmount);
        public decimal TaxAmount { get; set; }
        public Guid OrderId { get; set; }
        public OrderDto? Order { get; set; }

        public decimal DiscountAmount { get; set; }
        public List<InvoiceLineItemDto> InvoiceLineItems { get; set; } = [];
    }
}


