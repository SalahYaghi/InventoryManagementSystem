using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Inventory.Categories.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Inventory.Category;

public class CategoryMapperTests
{
    [Fact]
    public void ToDto_MapsAllProperties()
    {
        var entity = MapperTestData.Category();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Name, dto.Name);
    }
}
