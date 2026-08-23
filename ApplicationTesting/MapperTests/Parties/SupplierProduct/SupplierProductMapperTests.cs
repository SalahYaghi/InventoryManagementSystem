using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Parties.SupplierProducts.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Parties.SupplierProduct;

public class SupplierProductMapperTests
{
    [Fact]
    public void ToDto_MapsAllProperties()
    {
        var entity = MapperTestData.SupplierProduct();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.SupplierId, dto.SupplierId);
        Assert.Equal(entity.ProductId, dto.ProductId);
        Assert.Equal(entity.PurchasePrice, dto.PurchasePrice);
        Assert.Equal(entity.IsActive, dto.IsActive);
    }

    [Fact]
    public void ToDtoForList_MapsAllProperties()
    {
        var product = MapperTestData.Product();
        var entity = MapperTestData.SupplierProduct(product);
        var dto = entity.ToDtoForList();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.SupplierId, dto.SupplierId);
        Assert.Equal(entity.ProductId, dto.ProductId);
        Assert.Equal(entity.PurchasePrice, dto.PurchasePrice);
        Assert.Equal(entity.IsActive, dto.IsActive);
        Assert.Equal(product.ProductName, dto.ProductName);
        Assert.Equal(entity.CreatedAtUtc, dto.CreatedAt);
        Assert.Equal(entity.LastModifiedUtc, dto.UpdatedAt);
    }

    [Fact]
    public void ToDtoForList_DefaultsProductName_WhenNotLoaded()
    {
        var dto = MapperTestData.SupplierProduct().ToDtoForList();
        Assert.Equal(string.Empty, dto.ProductName);
    }
}
