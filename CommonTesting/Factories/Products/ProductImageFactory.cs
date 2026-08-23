using Domain.Products.Domain.Products;

namespace InventoryManagement.Tests.Common.Factories.Products;

public static class ProductImageFactory
{
    public static ProductImage CreateValid(Guid? id = null, Guid? productId = null, string imageUrl = "images/product.png")
    {
        var result = ProductImage.Create(id ?? Guid.NewGuid(), productId ?? Guid.NewGuid(), imageUrl);
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }
}
