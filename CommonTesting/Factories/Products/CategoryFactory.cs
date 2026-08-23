using Domain.Products.Category;

namespace InventoryManagement.Tests.Common.Factories.Products;

public static class CategoryFactory
{
    public static Category CreateValid(Guid? id = null, string name = "Spare Parts")
    {
        var result = Category.Create(id ?? Guid.NewGuid(), name);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
