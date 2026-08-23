using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;

namespace Domain.Invoices
{
    public static class LineItemErrors
    {
        public static readonly Error InvoiceRequired =
            Error.Validation("LineItem.InvoiceRequired", "Invoice is required.");

        public static readonly Error NameRequired =
            Error.Validation("LineItem.NameRequired", "Name is required.");

        public static readonly Error NameTooLong =
            Error.Validation("LineItem.NameTooLong", "Name exceeds maximum length.");

        public static readonly Error QuantityInvalid =
            Error.Validation("LineItem.QuantityInvalid", "Quantity must be greater than zero.");
        public static readonly Error UnitPriceInvalid =
            Error.Validation("LineItem.UnitPriceInvalid", "Unit price must be greater than or equal to zero.");

        public static readonly Error TaxInvalid =
            Error.Validation("LineItem.TaxInvalid", "Tax must be greater than or equal to zero.");
        public static readonly Error LineNoInvalid =
            Error.Validation("LineItem.LineNoInvalid", " Line No must be greater than or equal to zero.");

    }
}

