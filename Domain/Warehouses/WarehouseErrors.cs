using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;

namespace Domain.Warehouses
{
    public static class WarehouseErrors
    {
        public static readonly Error NameRequired =
            Error.Validation("Warehouse.NameRequired", "Warehouse name is required.");

        public static readonly Error NameTooLong =
            Error.Validation("Warehouse.NameTooLong", "Warehouse name exceeds maximum length.");

        public static readonly Error CodeRequired =
            Error.Validation("Warehouse.CodeRequired", "Warehouse code is required.");

        public static readonly Error CodeTooLong =
            Error.Validation("Warehouse.CodeTooLong", "Warehouse code exceeds maximum length.");

        public static readonly Error AddressRequired =
            Error.Validation("Warehouse.AddressRequired", "Address is required.");

        public static readonly Error InvalidStatus =
            Error.Validation("Warehouse.InvalidStatus", "Warehouse status is invalid.");
    }
}

