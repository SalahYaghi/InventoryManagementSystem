using Domain.Contacts.ContactInfo;

namespace InventoryManagement.Tests.Common.Factories.Contacts;

public static class ContactInfoFactory
{
    public static ContactInfo CreateValid(
        Guid? id = null,
        string email = "person@test.com",
        string phoneNumber = "+970599123456",
        string alternitavePhoneNumber = "+970598123456",
        string? faxNumber = null,
        string? websiteUrl = "https://example.com")
    {
        var result = ContactInfo.Create(
            id ?? Guid.NewGuid(),
            email,
            phoneNumber,
            alternitavePhoneNumber,
            faxNumber,
            websiteUrl);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
