using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Domain.Customer;
using InventoryManagement.Tests.Common.Factories.Contacts;

namespace InventoryManagement.Tests.Common.Factories.Customers;

public static class CustomerFactory
{
    public static Customer CreateValid(
        Guid? id = null,
        string customerName = "Cash Customer",
        string customerCode = "CUS-1",
        ContactInfo? contact = null,
        Address? address = null,
        string? notes = "Valid customer")
    {
        var result = Customer.Create(
            id ?? Guid.NewGuid(),
            customerName,
            customerCode,
            contact ?? ContactInfoFactory.CreateValid(),
            address ?? AddressFactory.CreateValid(),
            notes);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
