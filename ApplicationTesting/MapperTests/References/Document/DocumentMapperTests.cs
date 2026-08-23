using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.References.Documents.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.References.Document;

public class DocumentMapperTests
{
    [Fact]
    public void ToDto_MapsAllProperties()
    {
        var entity = MapperTestData.Document();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.DocumentType, dto.DocumentType);
    }
}
