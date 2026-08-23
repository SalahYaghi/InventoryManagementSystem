using Xunit;

using Domain.Products.Domain.Products; // ProductImage's (accidentally) nested namespace
using CategoryEntity = Domain.Products.Category.Category;

namespace InventoryManagement.Application.DomainTesting.Products;

public class CategoryTests
{
    [Fact]
    public void Create_WithValidName_Succeeds()
    {
        var result = CategoryEntity.Create(Guid.NewGuid(), "Electronics");

        Assert.True(result.IsSuccess);
        Assert.Equal("Electronics", result.Value.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_WithEmptyName_Fails(string? name)
    {
        var result = CategoryEntity.Create(Guid.NewGuid(), name!);
        Assert.True(result.IsError);
    }

    [Fact]
    public void Create_WithNameOver20Chars_Fails()
    {
        var result = CategoryEntity.Create(Guid.NewGuid(), new string('C', 21));
        Assert.True(result.IsError);
    }

    [Fact]
    public void Create_WithNameExactly20Chars_Succeeds()
    {
        var result = CategoryEntity.Create(Guid.NewGuid(), new string('C', 20));
        Assert.True(result.IsSuccess);
    }

 
    [Fact]
     public void Create_WithWhitespaceOnlyName_ShouldFail()
    {
        var result = CategoryEntity.Create(Guid.NewGuid(), "   ");

        Assert.True(result.IsError); // succeeds today
    }

    [Fact]
    public void Update_WithValidName_ChangesName()
    {
        var category = CategoryEntity.Create(Guid.NewGuid(), "Old").Value;

        var result = category.Update("New");

        Assert.True(result.IsSuccess);
        Assert.Equal("New", category.Name);
    }

    [Fact]
    public void Update_WithInvalidName_DoesNotMutate()
    {
        var category = CategoryEntity.Create(Guid.NewGuid(), "Old").Value;

        var result = category.Update(new string('C', 21));

        Assert.True(result.IsError);
        Assert.Equal("Old", category.Name);
    }
}

public class ProductImageTests
{
    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var result = ProductImage.Create(Guid.NewGuid(), Guid.NewGuid(), "https://cdn.x.com/a.png");
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_WithEmptyProductId_Fails()
    {
        var result = ProductImage.Create(Guid.NewGuid(), Guid.Empty, "https://cdn.x.com/a.png");
        Assert.True(result.IsError);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_WithMissingImageUrl_Fails(string? url)
    {
        var result = ProductImage.Create(Guid.NewGuid(), Guid.NewGuid(), url!);
        Assert.True(result.IsError);
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    // Because ValidationHelper.IsValidImageUrlOrPath accepts almost any string
    // (see ValidationHelperTests), garbage image "URLs" are stored.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Create_WithGarbageUrl_ShouldFail()
    {
        var result = ProductImage.Create(Guid.NewGuid(), Guid.NewGuid(),
            "definitely not a url or a path !!!");

        Assert.True(result.IsError); // succeeds today
    }

    [Fact]
    public void Update_WithValidData_ChangesFields()
    {
        var image = ProductImage.Create(Guid.NewGuid(), Guid.NewGuid(), "https://x.com/1.png").Value;
        var newProductId = Guid.NewGuid();

        var result = image.Update(newProductId, "https://x.com/2.png");

        Assert.True(result.IsSuccess);
        Assert.Equal(newProductId, image.ProductId);
        Assert.Equal("https://x.com/2.png", image.ImageUrl);
    }
}
