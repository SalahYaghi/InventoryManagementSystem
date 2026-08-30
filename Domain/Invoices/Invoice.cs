using Domain.Orders;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;
using System.Runtime.CompilerServices;

namespace Domain.Invoices
{
    public class Invoice : AuditableEntity
    {
        public Guid OrderId { get; private set; }
        public Domain.Orders.Order Order { get; private set; }

        public InvoiceStatus Status { get; private set; }
        public InvoiceType InvoiceType { get; private set; }

        public decimal NetAmount => SubTotalAmount + TaxAmount - DiscountAmount;
        public decimal SubTotalAmount => _lineItems.Sum(i => i.TotalAmount);
        public decimal TaxAmount => _lineItems.Sum(i => i.Tax);
        public decimal DiscountAmount { get; private set; }
        public string Notes => Order?.Notes ?? string.Empty;

        private readonly List<InvoiceLineItem> _lineItems = []; 
        public IReadOnlyCollection<InvoiceLineItem> LineItems => _lineItems;

        private Invoice() { }

        private Invoice(
            Guid id,
            InvoiceStatus status,
            InvoiceType invoiceType,
            decimal discountAmount,
            List<InvoiceLineItem>lineItems) : base(id)
        {
            Status = status;
            InvoiceType = invoiceType;
            DiscountAmount = discountAmount;
            _lineItems = lineItems;
        }

        public static Result<Invoice> Create(
            Guid id,
            InvoiceType invoiceType,
             decimal discountAmount,
            List<InvoiceLineItem>lineItems ,
            Guid orderId)
        {
            if (id == Guid.Empty)
                return InvoiceErrors.InvalidIdAssigned;

            if (orderId == Guid.Empty)
                return InvoiceErrors.OrderRequired;
      
            if (!Enum.IsDefined(typeof(InvoiceType), invoiceType))
                return InvoiceErrors.InvalidInvoiceType;

            if (discountAmount < 0)
                return InvoiceErrors.DiscountAmountInvalid;

            if (lineItems.Count == 0)
                return InvoiceErrors.InvoiceLineItemsRequired;

            decimal itemsTotal = lineItems.Sum(i => i.TotalAmount);
            if (itemsTotal < discountAmount)
                return InvoiceErrors.DiscountAmountInvalid;

            var invoice = new Invoice(
                id,
                InvoiceStatus.Issued,
                invoiceType,
                discountAmount,
                lineItems);

            invoice.OrderId = orderId;

            return invoice;
        }
        
    }
}

