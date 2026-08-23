using Domain.Identity.RefreshToken;

namespace InventoryManagement.Tests.Common.Factories.Identity;

public static class RefreshTokenFactory
{
    public static RefreshToken CreateValid(
        Guid? id = null,
        string token = "refresh-token-value",
        Guid? userId = null,
        DateTimeOffset? expiresAt = null)
    {
        var result = RefreshToken.Create(
            id ?? Guid.NewGuid(),
            token,
            userId ?? Guid.NewGuid(),
            expiresAt ?? DateTimeOffset.UtcNow.AddDays(7));

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
