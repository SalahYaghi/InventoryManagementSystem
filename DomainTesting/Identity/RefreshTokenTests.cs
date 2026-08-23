using RefreshTokenEntity = Domain.Identity.RefreshToken.RefreshToken;
using Domain.Identity.RefreshToken;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Identity;

public class RefreshTokenTests
{
    private static RefreshTokenEntity CreateValidToken() =>
        RefreshTokenEntity.Create(
            Guid.NewGuid(), "sample-token-value", Guid.NewGuid(),
            DateTimeOffset.UtcNow.AddDays(7)).Value!;

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var expiresAt = DateTimeOffset.UtcNow.AddDays(7);

        var result = RefreshTokenEntity.Create(id, "sample-token-value", userId, expiresAt);

        Assert.False(result.IsError);
        var token = result.Value!;
        Assert.Equal(id, token.Id);
        Assert.Equal("sample-token-value", token.Token);
        Assert.Equal(userId, token.UserId);
        Assert.Equal(expiresAt, token.ExpiresAt);
        Assert.Null(token.RevokedAt);
        Assert.False(token.IsRevoked);
        Assert.False(token.IsExpired);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithMissingToken_Fails(string? tokenValue)
    {
        var result = RefreshTokenEntity.Create(
            Guid.NewGuid(), tokenValue!, Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(7));

        Assert.True(result.IsError);
        Assert.Equal(RefreshTokenErrors.TokenIsRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyUserId_Fails()
    {
        var result = RefreshTokenEntity.Create(
            Guid.NewGuid(), "token", Guid.Empty, DateTimeOffset.UtcNow.AddDays(7));

        Assert.True(result.IsError);
        Assert.Equal(RefreshTokenErrors.UserIsRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithPastExpiry_Fails()
    {
        var result = RefreshTokenEntity.Create(
            Guid.NewGuid(), "token", Guid.NewGuid(), DateTimeOffset.UtcNow.AddMinutes(-1));

        Assert.True(result.IsError);
        Assert.Equal(RefreshTokenErrors.InvalidExpiratoinDate.Code, result.TopError.Code);
    }

    // ---------- Revoke ----------

    [Fact]
    public void Revoke_FirstTime_SetsRevokedAt()
    {
        var token = CreateValidToken();
        var before = DateTimeOffset.UtcNow;

        var result = token.Revoke();

        Assert.False(result.IsError);
        Assert.True(token.IsRevoked);
        Assert.NotNull(token.RevokedAt);
        Assert.True(token.RevokedAt >= before);
    }

    [Fact]
    public void Revoke_SecondTime_FailsWithAlreadyRevoked()
    {
        var token = CreateValidToken();
        token.Revoke();
        var firstRevokedAt = token.RevokedAt;

        var result = token.Revoke();

        Assert.True(result.IsError);
        Assert.Equal(RefreshTokenErrors.AlreadyRevoked.Code, result.TopError.Code);
        Assert.Equal(firstRevokedAt, token.RevokedAt); // timestamp untouched
    }

    // ---------- IsExpired ----------

    [Fact]
    public void IsExpired_ReflectsExpiresAt()
    {
        var token = CreateValidToken();
        Assert.False(token.IsExpired);

        // ExpiresAt has a public setter, so we can simulate the passage of time.
        token.ExpiresAt = DateTimeOffset.UtcNow.AddSeconds(-1);
        Assert.True(token.IsExpired);
    }

    // Design notes (not failing tests):
    // - Token, UserId, RevokedAt and ExpiresAt all have public setters, so a
    //   revoked/expired token can be trivially "un-revoked" by assignment.
    // - Revoke() succeeds on an EXPIRED token and, more importantly, an expired
    //   token that was never revoked reports IsRevoked == false; token-validity
    //   checks must remember to test both flags.
    // - RefreshToken.cs has a stray `using Microsoft.IdentityModel.Tokens;`
    //   which drags an Identity dependency into the Domain project.
    // - Error codes are misspelled: "RefershToken.*", "InvalidExpiratoinDate".
}
