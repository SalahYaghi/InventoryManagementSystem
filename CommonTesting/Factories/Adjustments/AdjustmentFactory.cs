using Domain.Adjustments;

namespace InventoryManagement.Tests.Common.Factories.Adjustments;

public static class AdjustmentFactory
{
    public static Adjustment CreateValid(
        Guid? id = null,
        Guid? warehouseId = null,
        AdjustmentReason adjustmentReason = AdjustmentReason.Damaged,
        List<AdjustmentDetail>? adjustmentDetails = null,
        AdjustmentType? adjustmentType = null,
        string? notes = "Valid adjustment")
    {
        var result = Adjustment.Create(
            id ?? Guid.NewGuid(),
            warehouseId ?? Guid.NewGuid(),
            adjustmentReason,
            adjustmentDetails ?? new List<AdjustmentDetail> { AdjustmentDetailFactory.CreateValid() },
            adjustmentType,
            notes);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
