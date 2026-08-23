using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Customer
{
 
        public static class CustomerErrors
        {
            public static readonly Error NameRequired =
                Error.Validation("Customer.NameRequired", "Customer name is required.");

            public static readonly Error NameTooLong =
                Error.Validation("Customer.NameTooLong", "Customer name is too long.");

            public static readonly Error CodeRequired =
                Error.Validation("Customer.CodeRequired", "Customer code is required.");

            public static readonly Error CodeTooLong =
                Error.Validation("Customer.CodeTooLong", "Customer code is too long.");

            public static readonly Error ContactRequired =
                Error.Validation("Customer.ContactRequired", "Contact is required.");

            public static readonly Error AddressRequired =
                Error.Validation("Customer.AddressRequired", "Address is required.");

            public static readonly Error NotesTooLong =
                Error.Validation("Customer.NotesTooLong", "Notes are too long.");
        }
    } 
