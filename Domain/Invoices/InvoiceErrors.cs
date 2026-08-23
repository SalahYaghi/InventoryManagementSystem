using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;

namespace Domain.Invoices
{
    public static class InvoiceErrors
    {
        public static readonly Error InvalidIdAssigned =
            Error.Validation("Invoice.InvalidIdAssigned", "Id assigned is invalid.");

        public static readonly Error InvalidOrderIdAssigned =
            Error.Validation("Invoice.InvalidOrderIdAssigned", "Order assigned is invalid.");

        public static readonly Error InvoiceLineItemsRequired =
            Error.Validation("Invoice.InvoiceLineItemsRequired", "Invoice line items are required.");

        public static readonly Error InvoiceNumberRequired =
            Error.Validation("Invoice.InvoiceNumberRequired", "Invoice number is required.");

        public static readonly Error InvoiceNumberTooLong =
            Error.Validation("Invoice.InvoiceNumberTooLong", "Invoice number exceeds maximum length.");

        public static readonly Error OrderRequired =
            Error.Validation("Invoice.OrderRequired", "Order is required.");

        public static readonly Error InvalidStatus =
            Error.Validation("Invoice.InvalidStatus", "Invoice status is invalid.");

        public static readonly Error InvalidInvoiceType =
            Error.Validation("Invoice.InvalidInvoiceType", "Invoice type is invalid.");

        public static readonly Error NetAmountInvalid =
            Error.Validation("Invoice.NetAmountInvalid", "Net amount must be greater than or equal to zero.");

        public static readonly Error SubTotalAmountInvalid =
            Error.Validation("Invoice.SubTotalAmountInvalid", "Subtotal amount must be greater than or equal to zero.");

        public static readonly Error TaxAmountInvalid =
            Error.Validation("Invoice.TaxAmountInvalid", "Tax amount must be greater than or equal to zero.");

        public static readonly Error DiscountAmountInvalid =
            Error.Validation("Invoice.DiscountAmountInvalid", "Discount amount must be greater than or equal to zero.");

        public static readonly Error NotesTooLong =
            Error.Validation("Invoice.NotesTooLong", "Notes exceeds maximum length.");
    }
}

