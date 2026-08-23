using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.References.Cities.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.References.City;

public class CityMapperTests
{
    [Fact]
    public void ToDto_MapsAllProperties()
    {
        var entity = MapperTestData.City();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Name, dto.Name);
    }
}
