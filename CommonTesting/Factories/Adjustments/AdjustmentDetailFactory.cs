using Domain.Adjustments;

namespace InventoryManagement.Tests.Common.Factories.Adjustments;

public static class AdjustmentDetailFactory
{
    public static AdjustmentDetail CreateValid(Guid? id = null, Guid? productId = null, decimal quantity = 3m)
    {
        var result = AdjustmentDetail.Create(id ?? Guid.NewGuid(), productId ?? Guid.NewGuid(), quantity);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
