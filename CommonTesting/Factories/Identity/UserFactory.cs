using Domain.Identity.Users;

namespace InventoryManagement.Tests.Common.Factories.Identity;

public static class UserFactory
{
    public static User CreateValid(string username = "salah_user", string hashedpassword = "hashed-password", string email = "user@test.com", Role role = Role.Admin, bool isActive = true, Guid? employeeId = null)
    {
        var result = User.Create(username, hashedpassword, email, role, isActive, employeeId ?? Guid.NewGuid());
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }
}
