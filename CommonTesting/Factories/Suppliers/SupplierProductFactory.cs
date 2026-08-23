using Domain.Suppliers.SupplierProducts;

namespace InventoryManagement.Tests.Common.Factories.Suppliers;

public static class SupplierProductFactory
{
    public static SupplierProduct CreateValid(
        Guid? id = null,
        Guid? supplierId = null,
        Guid? productId = null,
        decimal purchasePrice = 5m)
    {
        var result = SupplierProduct.Create(
            id ?? Guid.NewGuid(),
            supplierId ?? Guid.NewGuid(),
            productId ?? Guid.NewGuid(),
            purchasePrice);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
