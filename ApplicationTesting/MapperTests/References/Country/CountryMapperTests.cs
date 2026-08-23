using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.References.Countries.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.References.Country;

public class CountryMapperTests
{
    [Fact]
    public void ToDto_MapsAllProperties()
    {
        var entity = MapperTestData.Country();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Name, dto.Name);
    }
}
