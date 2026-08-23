using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.References.ContactInfos.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.References.ContactInfo;

public class ContactInfoMapperTests
{
    [Fact]
    public void ToDto_MapsAllProperties()
    {
        var entity = MapperTestData.Contact();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Email, dto.Email);
        Assert.Equal(entity.PhoneNumber, dto.PhoneNumber);
        Assert.Equal(entity.AlternitavePhoneNumber, dto.AlternitavePhoneNumber);
        Assert.Equal(entity.FaxNumber, dto.FaxNumber);
        Assert.Equal(entity.WebsiteUrl, dto.WebsiteUrl);
    }
}
