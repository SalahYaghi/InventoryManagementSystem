using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Warehouses;
using InventoryManagement.Tests.Common.Factories.Contacts;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Tests.Common.Factories.Warehouses;

public static class WarehouseFactory
{
    public static  Warehouse CreateValid(
        Guid? id = null,
        string name = "Main Warehouse",
        string code = "WH-1",
        Address? address = null )
    {

        var result = Warehouse.Create(id ?? Guid.NewGuid(), name, code, address ?? AddressFactory.CreateValid());

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
}
