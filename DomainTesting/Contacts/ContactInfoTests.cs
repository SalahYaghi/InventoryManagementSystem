using Domain.Common.Errors;
using ContactInfoEntity = Domain.Contacts.ContactInfo.ContactInfo;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Contacts;

public class ContactInfoTests
{
    private static ContactInfoEntity CreateValid() =>
        ContactInfoEntity.Create(
            Guid.NewGuid(),
            "user@example.com",
            "+972590000000",
            "0590000001",
            faxNumber: "022345678",
            websiteUrl: "https://example.com").Value!;

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var id = Guid.NewGuid();

        var result = ContactInfoEntity.Create(
            id, "user@example.com", "+972590000000", "0590000001", "022345678", "https://example.com");

        Assert.False(result.IsError);
        var contact = result.Value!;
        Assert.Equal(id, contact.Id);
        Assert.Equal("user@example.com", contact.Email);
        Assert.Equal("+972590000000", contact.PhoneNumber);
        Assert.Equal("0590000001", contact.AlternitavePhoneNumber);
        Assert.Equal("022345678", contact.FaxNumber);
        Assert.Equal("https://example.com", contact.WebsiteUrl);
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("missing-at.example.com")]
    [InlineData("")]
    public void Create_WithInvalidEmail_Fails(string email)
    {
        var result = ContactInfoEntity.Create(
            Guid.NewGuid(), email, "+972590000000", "", null, null);

        Assert.True(result.IsError);
        Assert.Equal(ValidationErrors.EmailInvalid.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData("123")]            // too short (< 7 digits)
    [InlineData("abcdefgh")]       // letters
    [InlineData("+12345678901234567")] // too long (> 15 digits)
    public void Create_WithInvalidPhone_Fails(string phone)
    {
        var result = ContactInfoEntity.Create(
            Guid.NewGuid(), "user@example.com", phone, "", null, null);

        Assert.True(result.IsError);
        Assert.Equal(ValidationErrors.PhoneNumberInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithInvalidAlternatePhone_Fails()
    {
        var result = ContactInfoEntity.Create(
            Guid.NewGuid(), "user@example.com", "+972590000000", "abc", null, null);

        Assert.True(result.IsError);
        Assert.Equal(ValidationErrors.PhoneNumberInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyAlternatePhone_Succeeds()
    {
        var result = ContactInfoEntity.Create(
            Guid.NewGuid(), "user@example.com", "+972590000000", "", null, null);

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_WithInvalidWebsiteUrl_Fails()
    {
        var result = ContactInfoEntity.Create(
            Guid.NewGuid(), "user@example.com", "+972590000000", "", null, "not a url");

        Assert.True(result.IsError);
        Assert.Equal(ValidationErrors.UrlInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullOptionalFields_Succeeds()
    {
        var result = ContactInfoEntity.Create(
            Guid.NewGuid(), "user@example.com", "+972590000000", "", null, null);

        Assert.False(result.IsError);
    }


    // ---------- Update ----------

    [Fact]
    public void Update_WithValidData_ReplacesAllFields()
    {
        var contact = CreateValid();

        var result = contact.Update(
            "new@example.com", "+972591111111", null, null, "https://new.example.com");

        Assert.False(result.IsError);
        Assert.Equal("new@example.com", contact.Email);
        Assert.Equal("+972591111111", contact.PhoneNumber);
        Assert.Null(contact.AlternitavePhoneNumber);
        Assert.Null(contact.FaxNumber);
        Assert.Equal("https://new.example.com", contact.WebsiteUrl);
    }

    [Fact]
    public void Update_WithInvalidEmail_FailsWithoutMutating()
    {
        var contact = CreateValid();

        var result = contact.Update("bad", "+972591111111", null, null, null);

        Assert.True(result.IsError);
        Assert.Equal(ValidationErrors.EmailInvalid.Code, result.TopError.Code);
        Assert.Equal("user@example.com", contact.Email);
        Assert.Equal("+972590000000", contact.PhoneNumber);
    }

    [Fact]
    public void Update_WithInvalidPhone_Fails()
    {
        var contact = CreateValid();

        var result = contact.Update("new@example.com", "xyz", null, null, null);

        Assert.True(result.IsError);
        Assert.Equal(ValidationErrors.PhoneNumberInvalid.Code, result.TopError.Code);
    }
}
