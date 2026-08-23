using Domain.AuditLoggs;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.AuditLoggs;

public class AuditLogTests
{
    // ---------- UserLoginAuditLog ----------

    [Fact]
    public void LoginLog_Create_WithValidData_Succeeds()
    {
        var userId = Guid.NewGuid();

        var result = UserLoginAuditLog.Create(
            userId, AuditActions.Login, "10.0.0.1", "TestAgent/1.0", success: true);

        Assert.False(result.IsError);
        var log = result.Value!;
        Assert.Equal(userId, log.UserId);
        Assert.Equal(AuditActions.Login, log.Action);
        Assert.Equal("10.0.0.1", log.IpAddress);
        Assert.Equal("TestAgent/1.0", log.UserAgent);
        Assert.True(log.IsSuccess);
        Assert.Null(log.ErrorMessage);
        Assert.NotEqual(Guid.Empty, log.Id);
        Assert.True(log.CreatedAtUtc <= DateTimeOffset.UtcNow);
    }

    [Fact]
    public void LoginLog_Create_WithEmptyUserId_Fails()
    {
        var result = UserLoginAuditLog.Create(
            Guid.Empty, AuditActions.Login, null, null, success: false);

        Assert.True(result.IsError);
        Assert.Equal("AuditLog.InvlidUserId", result.TopError.Code);
    }

    [Fact]
    public void LoginLog_Create_NormalizesEmptyIpToNull()
    {
        var log = UserLoginAuditLog.Create(
            Guid.NewGuid(), AuditActions.Login, "", null, true).Value!;

        Assert.Null(log.IpAddress);
    }

    [Fact]
    public void LoginLog_Create_AcceptsFailureWithErrorMessage()
    {
        var log = UserLoginAuditLog.Create(
            Guid.NewGuid(), AuditActions.Login, "10.0.0.1", "Agent",
            success: false, errorMessages: "invalid credentials").Value!;

        Assert.False(log.IsSuccess);
        Assert.Equal("invalid credentials", log.ErrorMessage);
    }

    // ---------- UserOperationsAuditLog ----------

    [Fact]
    public void OperationsLog_Create_WithValidData_Succeeds()
    {
        var userId = Guid.NewGuid();

        var result = UserOperationsAuditLog.Create(
            userId, "CreateOrderCommand", "10.0.0.1", "Agent", success: true);

        Assert.False(result.IsError);
        var log = result.Value!;
        Assert.Equal(userId, log.UserId);
        Assert.Equal("CreateOrderCommand", log.RequsetName);
    }

    [Fact]
    public void OperationsLog_Create_WithEmptyUserId_Fails()
    {
        var result = UserOperationsAuditLog.Create(
            Guid.Empty, "CreateOrderCommand", null, null, true);

        Assert.True(result.IsError);
    }

     
    [Fact]
     public void LoginLog_Create_WithUndefinedAction_ShouldFail_ButIsAccepted()
    {
        var result = UserLoginAuditLog.Create(
            Guid.NewGuid(), (AuditActions)999, null, null, true);

         Assert.True(result.IsError);
    }

 
}
