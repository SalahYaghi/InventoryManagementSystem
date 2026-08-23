using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Parties.Supplier.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Parties.Supplier;

public class SupplierMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Supplier();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.SupplierName, dto.SupplierName);
        Assert.Equal(entity.SupplierCode, dto.SupplierCode);
        Assert.Equal(entity.ContactId, dto.ContactId);
        Assert.Equal(entity.AddressId, dto.AddressId);
        Assert.Equal(entity.Status, dto.Status);
        Assert.Equal(entity.Notes, dto.Notes);
    }

    [Fact]
    public void ToDto_MapsContactAndAddress()
    {
        var c = MapperTestData.Contact();
        var a = MapperTestData.Address();
        var dto = MapperTestData.Supplier(c, a).ToDto();
        Assert.NotNull(dto.Contact);
        Assert.Equal(c.Id, dto.Contact!.Id);
        Assert.NotNull(dto.Address);
        Assert.Equal(a.Id, dto.Address!.Id);
    }
}
