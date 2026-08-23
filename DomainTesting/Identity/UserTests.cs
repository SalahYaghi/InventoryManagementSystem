using Domain.Identity.Employee;
using Domain.Identity.Users;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Identity;

public class UserTests
{
    private static User CreateValidUser() =>
        User.Create("ahmad_92", "$2a$11$hashedvalue", "user@example.com",
            Role.Admin, isAtive: true, employeeId: Guid.NewGuid()).Value!;

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var employeeId = Guid.NewGuid();

        var result = User.Create(
            "ahmad_92", "$2a$11$hashedvalue", "user@example.com",
            Role.SalesUser, isAtive: true, employeeId);

        Assert.False(result.IsError);
        var user = result.Value!;
        Assert.Equal("ahmad_92", user.Username);
        Assert.Equal("$2a$11$hashedvalue", user.HashedPassword);
        Assert.Equal("user@example.com", user.Email);
        Assert.Equal(Role.SalesUser, user.Role);
        Assert.True(user.IsActive);
        Assert.Equal(employeeId, user.EmployeeId);
    }

    [Fact]
    public void Create_WithEmptyEmployeeId_Fails()
    {
        var result = User.Create(
            "ahmad_92", "hash", "user@example.com", Role.Admin, true, Guid.Empty);

        Assert.True(result.IsError);
        Assert.Equal(EmployeeErrors.EmployeeIsRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithUndefinedRole_Fails()
    {
        var result = User.Create(
            "ahmad_92", "hash", "user@example.com", (Role)999, true, Guid.NewGuid());

        Assert.True(result.IsError);
        Assert.Equal(UserErrors.InvalidRoleValueSent.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData("abcd")]        // 4 chars — below 5-char minimum
    [InlineData("1username")]   // starts with a digit
    [InlineData("_username")]   // starts with underscore
    [InlineData("user name")]   // contains a space
    [InlineData("user-name")]   // contains a hyphen
    [InlineData("thisusernameiswaytoolong21")] // 21+ chars — above 20-char maximum
    public void Create_WithInvalidUsername_Fails(string username)
    {
        var result = User.Create(
            username, "hash", "user@example.com", Role.Admin, true, Guid.NewGuid());

        Assert.True(result.IsError);
        Assert.Equal(UserErrors.InvalidUsernameSent.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData("abcde")]                 // exactly 5 chars
    [InlineData("a1234")]                 // starts with letter, rest digits
    [InlineData("Ahmad_92")]              // underscore allowed after first char
    [InlineData("abcdefghijklmnopqrst")]  // exactly 20 chars
    public void Create_WithValidUsername_Succeeds(string username)
    {
        var result = User.Create(
            username, "hash", "user@example.com", Role.Admin, true, Guid.NewGuid());

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_WithInvalidEmail_Fails()
    {
        var result = User.Create(
            "ahmad_92", "hash", "not-an-email", Role.Admin, true, Guid.NewGuid());

        Assert.True(result.IsError);
        Assert.Equal(UserErrors.EmailNotValid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyPasswordHash_ShouldFail_ButIsAccepted()
    {
        var result = User.Create(
            "ahmad_92", "", "user@example.com", Role.Admin, true, Guid.NewGuid());

        Assert.True(result.IsError);
    }

    // ---------- Update / UpdatePassword ----------

    [Fact]
    public void Update_WithValidData_Succeeds()
    {
        var user = CreateValidUser();

        var result = user.Update("newname1", "new@example.com", false, Role.Viewer);

        Assert.False(result.IsError);
        Assert.Equal("newname1", user.Username);
        Assert.Equal("new@example.com", user.Email);
        Assert.False(user.IsActive);
        Assert.Equal(Role.Viewer, user.Role);
    }

    [Fact]
    public void Update_WithInvalidUsername_FailsWithoutMutating()
    {
        var user = CreateValidUser();

        var result = user.Update("ab", "new@example.com", false, Role.Viewer);

        Assert.True(result.IsError);
        Assert.Equal("ahmad_92", user.Username);
        Assert.Equal(Role.Admin, user.Role);
    }

    [Fact]
    public void Update_WithUndefinedRole_Fails()
    {
        var user = CreateValidUser();

        var result = user.Update("ahmad_92", "user@example.com", true, (Role)(-1));

        Assert.True(result.IsError);
        Assert.Equal(UserErrors.InvalidRoleValueSent.Code, result.TopError.Code);
    }

    [Fact]
    public void UpdatePassword_ReplacesHash()
    {
        // Note: UpdatePassword also accepts any string, including empty —
        // same gap as Create. Documented as current behavior.
        var user = CreateValidUser();

        var result = user.UpdatePassword("$2a$11$newhash");

        Assert.False(result.IsError);
        Assert.Equal("$2a$11$newhash", user.HashedPassword);
    }

    // Design note (not a failing test): Username, HashedPassword, Email, Role,
    // IsActive and EmployeeId all have public setters, so every rule above can
    // be bypassed by direct assignment. Consider private setters.
}
