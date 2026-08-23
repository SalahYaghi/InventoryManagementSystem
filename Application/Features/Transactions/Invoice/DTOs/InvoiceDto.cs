using Contract.Features.Transactions.Orders.DTOs;
using Domain.Invoices;

namespace Contract.Features.Transactions.Invoice.DTOs
{
    public sealed record InvoiceDto
    {
        public Guid InvoiceId { get;  set; }
        public string Status { get; set; } = string.Empty;
        public string InvoiceType { get; set; } = string.Empty;
        public decimal NetAmount { get; set; }// => SubTotalAmount + TaxAmount - DiscountAmount;
        public decimal SubTotalAmount { get; set; }//=> InvoiceLineItems.Sum(i => i.TotalAmount);
        public decimal TaxAmount { get; set; }// => InvoiceLineItems.Sum(i => i.Tax);
        public decimal DiscountAmount { get;  set; }
        public OrderDto? Order { get; set; }

        public Guid OrderId { get; set; }
        public List<InvoiceLineItemDto> InvoiceLineItems { get; set; } = [];

    }
}

