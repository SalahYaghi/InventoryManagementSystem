using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Parties.Customers.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Parties.Customer;

public class CustomerMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Customer();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.CustomerName, dto.CustomerName);
        Assert.Equal(entity.CustomerCode, dto.CustomerCode);
        Assert.Equal(entity.ContactId, dto.ContactId);
        Assert.Equal(entity.AddressId, dto.AddressId);
        Assert.Equal(entity.Notes, dto.Notes);
    }

    [Fact]
    public void ToDto_MapsContactAndAddress()
    {
        var c = MapperTestData.Contact();
        var a = MapperTestData.Address();
        var dto = MapperTestData.Customer(c, a).ToDto();
        Assert.NotNull(dto.Contact);
        Assert.Equal(c.Id, dto.Contact!.Id);
        Assert.NotNull(dto.Address);
        Assert.Equal(a.Id, dto.Address!.Id);
    }
}
