using Contract.Common.Interfaces;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Customer;
using Domain.Products;
using Domain.Products.Category;
using Domain.Suppliers;
using Domain.Suppliers.SupplierProducts;
using Domain.Warehouses;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Customers;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Suppliers;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace SubcutaneousTests.Features._Shared;

internal static class FeatureTestData
{
    public static async Task<(Country Country, City City, Address Address)> SeedAddressGraphAsync(IAppDbContext context)
    {
        var country = Country.Create($"Country-{Guid.NewGuid():N}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"City-{Guid.NewGuid():N}").Value;
        var address = AddressFactory.CreateValid(countryId: country.Id, cityId: city.Id);

        await context.Countries.AddAsync(country);
        await context.Cities.AddAsync(city);
        await context.Addresses.AddAsync(address);
        await context.SaveChangesAsync(CancellationToken.None);

        return (country, city, address);
    }

    public static async Task<Category> SeedCategoryAsync(IAppDbContext context)
    {
        var category = CategoryFactory.CreateValid(name: $"Category-{Guid.NewGuid():N}"[..30]);
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync(CancellationToken.None);
        return category;
    }

    public static async Task<Product> SeedProductAsync(IAppDbContext context, Guid? categoryId = null, string? sku = null)
    {
        var category = categoryId.HasValue
            ? await context.Categories.FirstAsync(x => x.Id == categoryId.Value)
            : await SeedCategoryAsync(context);

        var product = ProductFactory.CreateValid(
            sku: sku ?? $"S{Guid.NewGuid():N}"[..10],
            productName: $"Product-{Guid.NewGuid():N}"[..30],
            categoryId: category.Id,
            sellingPrice: 25m);

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync(CancellationToken.None);
        return product;
    }

    public static async Task<Warehouse> SeedWarehouseAsync(IAppDbContext context, Address? address = null)
    {
        if (address is null)
        {
            var graph = await SeedAddressGraphAsync(context);
            address = graph.Address;
        }

        var warehouse = WarehouseFactory.CreateValid(
            name: $"Warehouse-{Guid.NewGuid():N}"[..30],
            code: $"WH{Guid.NewGuid():N}"[..12],
            address: address);

        await context.Warehouses.AddAsync(warehouse);
        await context.SaveChangesAsync(CancellationToken.None);
        return warehouse;
    }

    public static async Task<WarehouseStock> SeedWarehouseStockAsync(
        IAppDbContext context,
        Guid warehouseId,
        Guid productId,
        decimal quantity = 20m)
    {
        var stock = WarehouseStockFactory.CreateValid(
            warehouseId: warehouseId,
            productId: productId,
            minimumStockLevel: 5m,
            quantity: quantity);

        await context.WarehouseStocks.AddAsync(stock);
        await context.SaveChangesAsync(CancellationToken.None);
        return stock;
    }

    public static async Task<Supplier> SeedSupplierAsync(IAppDbContext context, Address? address = null)
    {
        if (address is null)
        {
            var graph = await SeedAddressGraphAsync(context);
            address = graph.Address;
        }

        var contact = ContactInfoFactory.CreateValid(email: $"supplier{Guid.NewGuid():N}@test.com");
        var supplierResult = Supplier.Create(
            Guid.NewGuid(),
            $"Supplier-{Guid.NewGuid():N}"[..30],
            $"SUP{Guid.NewGuid():N}"[..12],
            contact,
            address,
            true,
            "Valid supplier");

        if (supplierResult.IsError)
            throw new InvalidOperationException(supplierResult.TopError.Description);

        var supplier = supplierResult.Value;

        await context.ContactInfos.AddAsync(contact);
        await context.Suppliers.AddAsync(supplier);
        await context.SaveChangesAsync(CancellationToken.None);
        return supplier;
    }

    public static async Task<Customer> SeedCustomerAsync(IAppDbContext context, Address? address = null)
    {
        if (address is null)
        {
            var graph = await SeedAddressGraphAsync(context);
            address = graph.Address;
        }

        var customer = CustomerFactory.CreateValid(
            customerName: $"Customer-{Guid.NewGuid():N}"[..30],
            customerCode: $"CUS{Guid.NewGuid():N}"[..12],
            contact: ContactInfoFactory.CreateValid(email: $"customer{Guid.NewGuid():N}@test.com"),
            address: address);

        await context.ContactInfos.AddAsync(customer.Contact!);
        await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync(CancellationToken.None);
        return customer;
    }

    public static async Task<SupplierProduct> SeedSupplierProductAsync(
        IAppDbContext context,
        Guid supplierId,
        Guid productId,
        decimal purchasePrice = 5m)
    {
        var supplierProduct = SupplierProductFactory.CreateValid(
            supplierId: supplierId,
            productId: productId,
            purchasePrice: purchasePrice);

        await context.SupplierProducts.AddAsync(supplierProduct);
        await context.SaveChangesAsync(CancellationToken.None);
        return supplierProduct;
    }
}
