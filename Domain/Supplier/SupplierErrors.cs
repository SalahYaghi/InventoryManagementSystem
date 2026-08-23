using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Suppliers
{
      
        public static class SupplierErrors
        {
            public static readonly Error NameRequired =
                Error.Validation("Supplier.NameRequired", "Supplier name is required.");

            public static readonly Error NameTooLong =
                Error.Validation("Supplier.NameTooLong", "Supplier name is too long.");

            public static readonly Error CodeRequired =
                Error.Validation("Supplier.CodeRequired", "Supplier code is required.");

            public static readonly Error CodeTooLong =
                Error.Validation("Supplier.CodeTooLong", "Supplier code is too long.");

            public static readonly Error ContactRequired =
                Error.Validation("Supplier.ContactRequired", "Contact is required.");

            public static readonly Error AddressRequired =
                Error.Validation("Supplier.AddressRequired", "Address is required.");

            public static readonly Error NotesTooLong =
                Error.Validation("Supplier.NotesTooLong", "Notes are too long.");
        }
    } 

