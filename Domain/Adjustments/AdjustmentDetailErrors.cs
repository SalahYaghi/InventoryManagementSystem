using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;

namespace Domain.Adjustments
{
    public static class AdjustmentDetailErrors
    {
        public static readonly Error AdjustmentRequired =
            Error.Validation("AdjustmentDetail.AdjustmentRequired", "Adjustment is required.");

        public static readonly Error ProductRequired =
            Error.Validation("AdjustmentDetail.ProductRequired", "Product is required.");

        public static readonly Error QuantityInvalid =
            Error.Validation("AdjustmentDetail.QuantityInvalid", "Quantity must be greater than zero.");
    }
}

