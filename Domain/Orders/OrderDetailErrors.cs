using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;

namespace Domain.Orders
{
    public static class OrderDetailErrors
    {
        public static readonly Error OrderRequired =
            Error.Validation("OrderDetail.OrderRequired", "Order is required.");

        public static readonly Error ProductRequired =
            Error.Validation("OrderDetail.ProductRequired", "Product is required.");

        public static readonly Error QuantityInvalid =
            Error.Validation("OrderDetail.QuantityInvalid", "Quantity must be greater than zero.");
   
        public static readonly Error ActualQuantityInvalid =
            Error.Validation("OrderDetail.ActualQuantityInvalid", "Actual quantity must be greater than or equal to zero.");

        public static readonly Error UnitPriceInvalid =
            Error.Validation("OrderDetail.UnitPriceInvalid", "Unit price must be greater than or equal to zero.");
    }
}

