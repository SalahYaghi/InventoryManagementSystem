using Domain.Contacts.Address;

namespace InventoryManagement.Tests.Common.Factories.Contacts;

public static class AddressFactory
{
    public static Address CreateValid(
        Guid? id = null,
        Guid? countryId = null,
        Guid? cityId = null,
        string? postalCode = "12345",
        string? buildingNumber = "10",
        string? street = "Main Street",
        string? description = "Valid address")
    {
        var result = Address.Create(
            id ?? Guid.NewGuid(),
            countryId ?? Guid.NewGuid(),
            cityId ?? Guid.NewGuid(),
            postalCode,
            buildingNumber,
            street,
            description);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
