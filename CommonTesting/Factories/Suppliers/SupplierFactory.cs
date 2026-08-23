using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Domain.Suppliers;
using InventoryManagement.Tests.Common.Factories.Contacts;

namespace InventoryManagement.Tests.Common.Factories.Suppliers;

public static class SupplierFactory
{
    public static Supplier CreateValid(
        Guid? id = null,
        string supplierName = "Al Quds Supplier",
        string supplierCode = "SUP-1",
        ContactInfo? contact = null,
        Address? address = null,
        string? notes = "Valid supplier")
    {
        var result = Supplier.Create(
            id ?? Guid.NewGuid(),
            supplierName,
            supplierCode,
            contact ?? ContactInfoFactory.CreateValid(),
            address ?? AddressFactory.CreateValid(),
            false,
            notes);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
