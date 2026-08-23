using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Parties.Employees.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Parties.Employee;

public class EmployeeMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Employee();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.JobTitle, dto.JobTitle);
        Assert.Equal(entity.PersonId, dto.PersonId);
        Assert.Equal(entity.WarehouseId, dto.WarehouseId);
        Assert.Equal(entity.HiringDate, dto.HiringDate);
    }

    [Fact]
    public void ToDto_MapsPersonDto_WhenLoaded()
    {
        var person = MapperTestData.Person();
        var dto = MapperTestData.Employee(person).ToDto();
        Assert.NotNull(dto.Person);
        Assert.Equal(person.Id, dto.Person!.Id);
    }

    [Fact]
    public void ToDto_MapsWarehouseDto_WhenLoaded()
    {
        var wh = MapperTestData.Warehouse();
        var dto = MapperTestData.Employee(warehouse: wh).ToDto();
        Assert.NotNull(dto.Warehouse);
        Assert.Equal(wh.Id, dto.Warehouse!.Id);
    }

    [Fact]
    public void ToDto_LeavesNavigationsNull_WhenNotLoaded()
    {
        var dto = MapperTestData.Employee().ToDto();
        Assert.Null(dto.Warehouse);
    }
}
