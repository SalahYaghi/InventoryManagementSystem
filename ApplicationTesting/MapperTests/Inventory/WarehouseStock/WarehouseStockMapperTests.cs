using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Inventory.WarehouseStocks.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Inventory.WarehouseStock;

public class WarehouseStockMapperTests
{
    [Fact]
    public void ToDto_MapsAllProperties()
    {
        var entity = MapperTestData.WarehouseStock();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.WarehouseId, dto.WarehouseId);
        Assert.Equal(entity.ProductId, dto.ProductId);
        Assert.Equal(entity.Quantity, dto.Quantity);
        Assert.Equal(entity.MinimumStockLevel, dto.MinimumStockLevel);
        Assert.Equal(entity.RowVersion, dto.RowVersion);
    }

    [Fact]
    public void ToDtoForList_MapsStockAndProductProperties()
    {
        var product = MapperTestData.Product();
        var entity = MapperTestData.WarehouseStock(product);
        var dto = entity.ToDtoForList();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Quantity, dto.Quantity);
        Assert.Equal(entity.MinimumStockLevel, dto.MinimumStockLevel);
        Assert.Equal(product.Id, dto.ProductId);
        Assert.Equal(product.SKU, dto.SKU);
        Assert.Equal(product.ProductName, dto.ProductName);
        Assert.Equal(product.SellingPrice, dto.SellingPrice);
        Assert.Equal(product.IsActive, dto.IsActive);
        Assert.Equal(product.Unit.ToString(), dto.Unit);
        Assert.Equal(product.Category!.Name, dto.Category);
    }
}
