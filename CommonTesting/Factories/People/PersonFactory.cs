using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Domain.People;
using InventoryManagement.Tests.Common.Factories.Contacts;

namespace InventoryManagement.Tests.Common.Factories.People;

public static class PersonFactory
{
    public static Person CreateValid(
        Guid? id = null,
        string nationalNo = "123456789",
        string firstName = "Salah",
        string secondName = "Mohd",
        string? thirdName = "Ali",
        string lastName = "Ahmad",
        bool gender = true,
        DateOnly? dateOfBirth = null,
        ContactInfo? contact = null,
        Address? address = null)
    {
        var result = Person.Create(
            id ?? Guid.NewGuid(),
            nationalNo,
            firstName,
            secondName,
            thirdName,
            lastName,
            gender,
            dateOfBirth ?? new DateOnly(2000, 1, 1),
            contact ?? ContactInfoFactory.CreateValid(),
            address ?? AddressFactory.CreateValid());

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
