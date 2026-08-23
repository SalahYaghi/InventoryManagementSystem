using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;

namespace Domain.Adjustments
{
    public static class AdjustmentErrors
    {
        public static readonly Error AdjusmentIsLocked =
           Error.Conflict("Adjusment.AdjusmentIsLocked", "Adjusment is locked can't be modiefied.");

        public static readonly Error AdjustmentDetailsRequired =
            Error.Validation("Adjustment.AdjustmentDetailsRequired", "adjustment details is required.");

        public static readonly Error WarehouseRequired =
            Error.Validation("Adjustment.WarehouseRequired", "Warehouse is required.");

        public static readonly Error InvalidAdjustmentType =
            Error.Validation("Adjustment.InvalidAdjustmentType", "Adjustment type is invalid.");

        public static readonly Error InvalidAdjustmentReason =
            Error.Validation("Adjustment.InvalidAdjustmentReason", "Adjustment reason is invalid.");

        public static readonly Error InvalidAdjustmentStatus =
            Error.Validation("Adjustment.InvalidAdjustmentStatus", "Adjustment status is invalid.");

        public static readonly Error NotesTooLong =
            Error.Validation("Adjustment.NotesTooLong", "Notes exceeds maximum length.");
    }
}

