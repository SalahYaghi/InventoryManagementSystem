using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Parties.People.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Parties.Person;

public class PersonMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Person();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.NationalNo, dto.NationalNo);
        Assert.Equal(entity.FirstName, dto.FirstName);
        Assert.Equal(entity.SecondName, dto.SecondName);
        Assert.Equal(entity.ThirdName, dto.ThirdName);
        Assert.Equal(entity.LastName, dto.LastName);
        Assert.Equal(entity.Gender, dto.Gender);
        Assert.Equal(entity.DateOfBirth, dto.DateOfBirth);
        Assert.Equal(entity.ContactId, dto.ContactId);
        Assert.Equal(entity.AddressId, dto.AddressId);
        Assert.Equal(entity.DocumentId, dto.DocumentId);
    }

    [Fact]
    public void ToDto_MapsNavigations_WhenLoaded()
    {
        var c = MapperTestData.Contact();
        var a = MapperTestData.Address();
        var d = MapperTestData.Document();
        var dto = MapperTestData.Person(c, a, d).ToDto();
        Assert.NotNull(dto.Contact);
        Assert.Equal(c.Id, dto.Contact!.Id);
        Assert.NotNull(dto.Address);
        Assert.Equal(a.Id, dto.Address!.Id);
        Assert.NotNull(dto.Document);
        Assert.Equal(d.Id, dto.Document!.Id);
    }
}
