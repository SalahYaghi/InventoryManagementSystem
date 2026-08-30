using Inventory.Domain.Common;
using System;
using Inventory.Domain.Common.Results;
using System.ComponentModel;

namespace Domain.Invoices
{


    public class InvoiceLineItem 
    {
        public int LineNo { get;private set; }
        public Guid InvoiceId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public decimal Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalAmount => UnitPrice * Quantity;
        public decimal Tax { get; private set; }
        private InvoiceLineItem() { }

        private InvoiceLineItem(
            int lineNo , 
            Guid invoiceId,
            string description,
            decimal quantity,
            decimal tax,
            decimal unitPrice) 
        {
            LineNo = lineNo;
            InvoiceId = invoiceId;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Tax = tax;
        }

        public static Result<InvoiceLineItem> Create(
            int lineNo,
            Guid invoiceId,
            string name,
            decimal tax,
            decimal quantity,
            decimal unitPrice)
        {
            if (lineNo < 0)
                return LineItemErrors.LineNoInvalid;

            if (invoiceId == Guid.Empty)
                return LineItemErrors.InvoiceRequired;

            if (string.IsNullOrWhiteSpace(name))
                return LineItemErrors.NameRequired;

            if (name.Length > 100)
                return LineItemErrors.NameTooLong;

            if (quantity <= 0)
                return LineItemErrors.QuantityInvalid;

            if (tax < 0)
                return LineItemErrors.TaxInvalid;

            if (unitPrice < 0)
                return LineItemErrors.UnitPriceInvalid;

            var lineItem = new InvoiceLineItem(
                lineNo,
                invoiceId,
                name,
                quantity,
                tax,
                unitPrice);

            return lineItem;
        }
        public Result<Updated> Update(
            Guid invoiceId,
            string name,
            decimal quantity,
            decimal unitPrice)
        {
            if (invoiceId == Guid.Empty)
                return LineItemErrors.InvoiceRequired;

            if (string.IsNullOrWhiteSpace(name))
                return LineItemErrors.NameRequired;

            if (name.Length > 100)
                return LineItemErrors.NameTooLong;

            if (quantity <= 0)
                return LineItemErrors.QuantityInvalid;

            if (unitPrice < 0)
                return LineItemErrors.UnitPriceInvalid;

            InvoiceId = invoiceId;
            Description = name;
            Quantity = quantity;
            UnitPrice = unitPrice;

            return Result.Updated;
        }

    }
}
