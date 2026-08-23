using Domain.Products;
using Domain.Products.Enums;

namespace InventoryManagement.Tests.Common.Factories.Products;

public static class ProductFactory
{
    public static Product CreateValid(
        Guid? id = null,
        string sku = "SKU-1",
        string? barCode = "123456789",
        string productName = "Engine Oil",
        string? description = "Valid product",
        Guid? categoryId = null,
        decimal sellingPrice = 10m,
        bool isActive = true,
        Unit unit = Unit.Piece)
    {
        var result = Product.Create(
            id ?? Guid.NewGuid(),
            sku,
            barCode,
            productName,
            description,
            categoryId ?? Guid.NewGuid(),
            sellingPrice,
            isActive,
            unit);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
