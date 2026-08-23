using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.References.Addresses.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.References.Address;

public class AddressMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Address();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.CountryId, dto.CountryId);
        Assert.Equal(entity.CityId, dto.CityId);
        Assert.Equal(entity.PostalCode, dto.PostalCode);
        Assert.Equal(entity.BuildingNumber, dto.BuildingNumber);
        Assert.Equal(entity.Street, dto.Street);
        Assert.Equal(entity.Description, dto.Description);
    }

    [Fact]
    public void ToDto_MapsCountryAndCity_WhenLoaded()
    {
        var country = MapperTestData.Country();
        var city = MapperTestData.City();
        var entity = MapperTestData.Address(country, city);
        var dto = entity.ToDto();
        Assert.NotNull(dto.Country);
        Assert.Equal(country.Id, dto.Country!.Id);
        Assert.NotNull(dto.City);
        Assert.Equal(city.Id, dto.City!.Id);
    }

    [Fact]
    public void ToDto_LeavesNavigationsNull_WhenNotLoaded()
    {
        var entity = MapperTestData.Address();
        var dto = entity.ToDto();
        Assert.Null(dto.Country);
        Assert.Null(dto.City);
    }
}
