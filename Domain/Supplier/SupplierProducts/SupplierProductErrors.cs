using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Suppliers.SupplierProducts
{
    public static class SupplierProductErrors
    {
        public static readonly Error SupplierRequired =
            Error.Validation("SupplierProduct.SupplierRequired", "Supplier is required.");

        public static readonly Error ProductRequired =
            Error.Validation("SupplierProduct.ProductRequired", "Product is required.");

        public static readonly Error InvalidPrice =
            Error.Validation("SupplierProduct.InvalidPrice", "Purchase price must be >= 0.");
    }
}

