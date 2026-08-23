using Domain.Products;
using Domain.Products.Enums;
using Xunit;

// ProductImage lives in an accidentally nested namespace (see file review notes)
using Domain.Products.Domain.Products;

namespace InventoryManagement.Application.DomainTesting.Products;

public class ProductTests
{
    private static Result_Helpers.ProductArgs Valid() => new();

    // Small mutable arg bag so each test changes exactly one thing.
    private static class Result_Helpers
    {
        public class ProductArgs
        {
            public Guid Id = Guid.NewGuid();
            public string Sku = "SKU-001";
            public string? BarCode = "1234567890";
            public string Name = "Test Product";
            public string? Description = "A product";
            public Guid CategoryId = Guid.NewGuid();
            public decimal SellingPrice = 9.99m;
            public bool IsActive = true;
            public Unit Unit = Unit.Piece;
        }
    }

    private static MechanicShop.Domain.Common.Results.Result<Product> Create(Result_Helpers.ProductArgs a)
        => Product.Create(a.Id, a.Sku, a.BarCode, a.Name, a.Description,
                          a.CategoryId, a.SellingPrice, a.IsActive, a.Unit);

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var result = Create(Valid());

        Assert.True(result.IsSuccess);
        Assert.Equal("SKU-001", result.Value.SKU);
        Assert.Equal(Unit.Piece, result.Value.Unit);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithMissingSku_Fails(string? sku)
    {
        var args = Valid(); args.Sku = sku!;
        var result = Create(args);

        Assert.True(result.IsError);
        Assert.Equal(ProductErrors.SKURequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithSkuOver10Chars_Fails()
    {
        var args = Valid(); args.Sku = new string('X', 11);
        var result = Create(args);

        Assert.True(result.IsError);
        Assert.Equal(ProductErrors.SKUTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithSkuExactly10Chars_Succeeds()
    {
        var args = Valid(); args.Sku = new string('X', 10);
        Assert.True(Create(args).IsSuccess);
    }

    [Fact]
    public void Create_WithBarCodeOver50Chars_Fails()
    {
        var args = Valid(); args.BarCode = new string('1', 51);
        Assert.Equal(ProductErrors.BarCodeTooLong.Code, Create(args).TopError.Code);
    }

    [Fact]
    public void Create_WithNullBarCode_Succeeds()
    {
        var args = Valid(); args.BarCode = null;
        Assert.True(Create(args).IsSuccess);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Create_WithMissingName_Fails(string? name)
    {
        var args = Valid(); args.Name = name!;
        Assert.Equal(ProductErrors.ProductNameRequired.Code, Create(args).TopError.Code);
    }

    [Fact]
    public void Create_WithNameOver30Chars_Fails()
    {
        var args = Valid(); args.Name = new string('N', 31);
        Assert.Equal(ProductErrors.ProductNameTooLong.Code, Create(args).TopError.Code);
    }

    [Fact]
    public void Create_WithDescriptionOver500Chars_Fails()
    {
        var args = Valid(); args.Description = new string('D', 501);
        Assert.Equal(ProductErrors.DescriptionTooLong.Code, Create(args).TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyCategoryId_Fails()
    {
        var args = Valid(); args.CategoryId = Guid.Empty;
        Assert.Equal(ProductErrors.CategoryRequired.Code, Create(args).TopError.Code);
    }

    [Fact]
    public void Create_WithNegativePrice_Fails()
    {
        var args = Valid(); args.SellingPrice = -0.01m;
        Assert.Equal(ProductErrors.InvalidPrice.Code, Create(args).TopError.Code);
    }

    [Fact]
    public void Create_WithZeroPrice_Succeeds()
    {
        // Documents the current business rule: price 0 is allowed.
        // If products must always cost something, change the rule and this test.
        var args = Valid(); args.SellingPrice = 0m;
        Assert.True(Create(args).IsSuccess);
    }

    [Fact]
    public void Create_WithUndefinedUnit_Fails()
    {
        var args = Valid(); args.Unit = (Unit)99;
        Assert.Equal(ProductErrors.InvalidUnit.Code, Create(args).TopError.Code);
    }

    [Fact]
    public void Update_WithValidData_ChangesAllFields()
    {
        var product = Create(Valid()).Value;
        var newCategory = Guid.NewGuid();

        var result = product.Update("NEW-SKU", "999", "New Name", "New desc",
                                    newCategory, 20m, false, Unit.Box);

        Assert.True(result.IsSuccess);
        Assert.Equal("NEW-SKU", product.SKU);
        Assert.Equal("New Name", product.ProductName);
        Assert.Equal(newCategory, product.CategoryId);
        Assert.Equal(20m, product.SellingPrice);
        Assert.False(product.IsActive);
        Assert.Equal(Unit.Box, product.Unit);
    }

    [Fact]
    public void Update_WithInvalidData_DoesNotMutate()
    {
        var product = Create(Valid()).Value;
        var originalSku = product.SKU;

        var result = product.Update("", null, "Name", null,
                                    Guid.NewGuid(), 5m, true, Unit.Piece);

        Assert.True(result.IsError);
        Assert.Equal(originalSku, product.SKU);
    }

    [Fact]
    public void AddProductImage_AddsToCollection()
    {
        var product = Create(Valid()).Value;
        var image = ProductImage.Create(Guid.NewGuid(), product.Id, "https://x.com/i.png").Value;

        var result = product.AddProductImage(image);

        Assert.True(result.IsSuccess);
        Assert.Single(product.ProductImages);
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    // AddProductImage does not null-check; adding null corrupts the collection.
   
    [Fact]
    public void AddProductImage_WithNull_ShouldReturnError()
    {
        var product = Create(Valid()).Value;

        var result = product.AddProductImage(null!); 
        
        Assert.True(result.IsError);
        Assert.Empty(product.ProductImages);
    }
}
