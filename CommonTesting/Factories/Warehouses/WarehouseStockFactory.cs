using Domain.Warehouses;

namespace InventoryManagement.Tests.Common.Factories.Warehouses;

public static class WarehouseStockFactory
{
    public static WarehouseStock CreateValid(
        Guid? id = null,
        Guid? warehouseId = null,
        Guid? productId = null,
        decimal minimumStockLevel = 5m,
        decimal quantity = 20m)
    {
        var result = WarehouseStock.Create(
            id ?? Guid.NewGuid(),
            warehouseId ?? Guid.NewGuid(),
            productId ?? Guid.NewGuid(),
            minimumStockLevel,
            quantity);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
