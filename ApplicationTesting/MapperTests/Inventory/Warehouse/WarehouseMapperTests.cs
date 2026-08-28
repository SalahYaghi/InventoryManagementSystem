using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Inventory.Warehouses.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Inventory.Warehouse;

public class WarehouseMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Warehouse();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Name, dto.Name);
        Assert.Equal(entity.Code, dto.Code);
        Assert.Equal(entity.AddressId, dto.AddressId);
        Assert.Equal(entity.WarehouseStatus, dto.WarehouseStatus);
    }

    [Fact]
    public void ToDto_MapsAddressDto_WhenLoaded()
    {
        var address = MapperTestData.Address();
        var entity = MapperTestData.Warehouse(address);
        var dto = entity.ToDto();
        Assert.NotNull(dto.Address);
        Assert.Equal(address.Id, dto.Address!.Id);
    }

    [Fact]
    public void ToDtoForList_MapsAllProperties()
    {
        var address = MapperTestData.Address();
        var entity = MapperTestData.Warehouse(address);
        var dto = entity.ToDtoForList();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Name, dto.Name);
        Assert.Equal(entity.Code, dto.Code);
        Assert.True(dto.IsActived);
        Assert.Equal(address.BuildingNumber, dto.BuildingNumber);
        Assert.Equal(address.Street, dto.Street);
    }

  
}
