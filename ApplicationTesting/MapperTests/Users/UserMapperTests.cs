using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Users.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Users;

public class UserMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.User();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.Username, dto.Username);
        Assert.Equal(entity.Email, dto.Email);
        Assert.Equal(entity.Role, dto.Role);
        Assert.Equal(entity.IsActive, dto.IsActive);
        Assert.Equal(entity.EmployeeId, dto.EmployeeId);
        Assert.Equal(entity.LastLoginAt, dto.LastLoginAt);
    }

    [Fact]
    public void ToDto_MapsId_NotGuidEmpty()
    {
        var dto = MapperTestData.User().ToDto();
        Assert.NotEqual(Guid.Empty, dto.Id);
    }

    [Fact]
    public void ToDto_MapsEmployeeDto_WhenLoaded()
    {
        var emp = MapperTestData.Employee();
        var dto = MapperTestData.User(emp).ToDto();
        Assert.NotNull(dto.Employee);
        Assert.Equal(emp.Id, dto.Employee!.Id);
    }

    [Fact]
    public void ToDto_LeavesEmployeeNull_WhenNotLoaded()
    {
        var dto = MapperTestData.User().ToDto();
        Assert.Null(dto.Employee);
    }
}
