using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Inventory.Product.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Inventory.Product;

public class ProductMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Product();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.SKU, dto.SKU);
        Assert.Equal(entity.BarCode, dto.BarCode);
        Assert.Equal(entity.ProductName, dto.ProductName);
        Assert.Equal(entity.Description, dto.Description);
        Assert.Equal(entity.SellingPrice, dto.SellingPrice);
        Assert.Equal(entity.IsActive, dto.IsActive);
        Assert.Equal(entity.Unit, dto.Unit);
        Assert.Equal(entity.CategoryId, dto.CategoryId);
    }

    [Fact]
    public void ToDto_MapsCategoryDto_WhenLoaded()
    {
        var entity = MapperTestData.Product();
        var dto = entity.ToDto();
        Assert.NotNull(dto.Category);
        Assert.Equal(entity.Category!.Id, dto.Category!.Id);
    }

    [Fact]
    public void ToListDto_MapsAllProperties()
    {
        var entity = MapperTestData.Product();
        var dto = entity.ToListDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.SKU, dto.SKU);
        Assert.Equal(entity.ProductName, dto.ProductName);
        Assert.Equal(entity.SellingPrice, dto.SellingPrice);
        Assert.Equal(entity.IsActive, dto.IsActive);
        Assert.Equal(entity.Unit.ToString(), dto.Unit);
        Assert.Equal(entity.Category?.Name, dto.Category);
    }
}
