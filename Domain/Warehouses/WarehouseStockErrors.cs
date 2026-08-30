using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;

namespace Domain.Warehouses
{
    public static class WarehouseStockErrors
    {
        public static readonly Error WarehouseRequired =
            Error.Validation("WarehouseStock.WarehouseRequired", "Warehouse is required.");

        public static readonly Error ProductRequired =
            Error.Validation("WarehouseStock.ProductRequired", "Product is required.");

        public static readonly Error QuantityInvalid =
            Error.Validation("WarehouseStock.QuantityInvalid", "Quantity must be greater than or equal to zero.");
        public static readonly Error QuantityExccededAlowedAmount=
            Error.Conflict("WarehouseStock.QuantityExccededAmount", "Quantity assigned is more than allowed quantity.");

        public static readonly Error MinimumStockLevelInvalid =
            Error.Validation("WarehouseStock.MinimumStockLevelInvalid", "Minimum stock level must be greater than or equal to zero.");
    }
}

