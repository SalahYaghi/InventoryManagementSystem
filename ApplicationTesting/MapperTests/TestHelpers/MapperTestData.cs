using System.Reflection;
using Domain.Adjustments;
using Domain.Contacts.Address.Country;
using Domain.Identity.Users;
using Domain.Invoices;
using Domain.Orders;
using Domain.Products.Enums;
using Domain.Warehouses;

using AddressEntity = Domain.Contacts.Address.Address;
using CategoryEntity = Domain.Products.Category.Category;
using ContactInfoEntity = Domain.Contacts.ContactInfo.ContactInfo;
using CustomerEntity = Domain.Customer.Customer;
using DocumentEntity = Domain.Document.Document;
using DocumentTypeEnum = Domain.Document.DocumentType;
using EmployeeEntity = Domain.Identity.Employee.Employee;
using PersonEntity = Domain.People.Person;
using ProductEntity = Domain.Products.Product;
using SupplierEntity = Domain.Suppliers.Supplier;
using SupplierProductEntity = Domain.Suppliers.SupplierProducts.SupplierProduct;
using ProductImageEntity = Domain.Products.Domain.Products.ProductImage;

namespace Application.UnitTests.Mappers.TestHelpers;

internal static class MapperTestData
{
    private static void SetNav<TEntity>(TEntity entity, string prop, object? value)
    {
        var pi = typeof(TEntity).GetProperty(prop, BindingFlags.Public | BindingFlags.Instance)
            ?? throw new InvalidOperationException($"{typeof(TEntity).Name}.{prop} not found");
        var setter = pi.GetSetMethod(true) ?? pi.GetSetMethod(false)
            ?? throw new InvalidOperationException($"{typeof(TEntity).Name}.{prop} has no setter");
        setter.Invoke(entity, [value]);
    }

    public static CategoryEntity Category()
        => CategoryEntity.Create(Guid.NewGuid(), "Electronics").Value!;

    public static ProductEntity Product(CategoryEntity? category = null)
    {
        var cat = category ?? Category();
        var p = ProductEntity.Create(Guid.NewGuid(), "SKU-001", "BC-001", "Widget", "A widget",
            cat.Id, 25.50m, true, Unit.Piece).Value!;
        SetNav(p, nameof(ProductEntity.Category), cat);
        return p;
    }

    public static ContactInfoEntity Contact()
        => ContactInfoEntity.Create(Guid.NewGuid(), "user@example.com", "+972590000000",
            "+972590000001", "022345678", "https://example.com").Value!;

    public static AddressEntity Address(Country? country = null, City? city = null)
    {
        var a = AddressEntity.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            "12345", "10A", "Main St", "Near the market").Value!;
        a.Country = country;
        a.City = city;
        return a;
    }

    public static Country Country() => Domain.Contacts.Address.Country.Country.Create("Palestine").Value;
    public static City City() => Domain.Contacts.Address.Country.City.Create( id : Guid.NewGuid() , countryId:Guid.NewGuid() , name: "Nablus").Value;

    public static DocumentEntity Document()
        => DocumentEntity.Create(Guid.NewGuid(), DocumentTypeEnum.Passport, "https://img.example.com/doc.png").Value!;

    public static CustomerEntity Customer(ContactInfoEntity? contact = null, AddressEntity? address = null)
        => CustomerEntity.Create(Guid.NewGuid(), "Acme Corp", "CUST-001",
            contact ?? Contact(), address ?? Address(), "preferred customer").Value!;

    public static SupplierEntity Supplier(ContactInfoEntity? contact = null, AddressEntity? address = null)
        => SupplierEntity.Create(Guid.NewGuid(), "Parts Ltd", "SUP-001",
            contact ?? Contact(), address ?? Address(), true, "main supplier").Value!;

    public static SupplierProductEntity SupplierProduct(ProductEntity? product = null)
    {
        var sp = SupplierProductEntity.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 15.00m).Value!;
        sp.Product = product;
        return sp;
    }

    public static PersonEntity Person(
        ContactInfoEntity? contact = null, AddressEntity? address = null, DocumentEntity? document = null)
    {
        var p = PersonEntity.Create(Guid.NewGuid(), "1234567890", "Ahmad", "Sami", "Khalid", "Yousef",
            true, new DateOnly(1990, 5, 1), contact ?? Contact(), address ?? Address()).Value!;
        if (document != null) p.UpdateDocument(document);
        return p;
    }

    public static EmployeeEntity Employee(PersonEntity? person = null, Warehouse? warehouse = null)
    {
        var pe = person ?? Person();
        var whId = warehouse?.Id ?? Guid.NewGuid();
        var emp = EmployeeEntity.Create("Storekeeper", pe, new DateOnly(2024, 1, 15), whId).Value!;
        emp.Warehouse = warehouse;
        return emp;
    }

    public static User User(EmployeeEntity? employee = null)
    {
        var empId = employee?.Id ?? Guid.NewGuid();
        var u = Domain.Identity.Users.User.Create("ahmad_92", "$2a$11$hash", "user@example.com",
            Role.Admin, true, empId).Value!;
        u.Employee = employee;
        return u;
    }

    public static Warehouse Warehouse(AddressEntity? address = null)
        => Domain.Warehouses.Warehouse.Create(Guid.NewGuid(), "Main Warehouse", "WH-001",
            address ?? Address()).Value!;

    public static WarehouseStock WarehouseStock(ProductEntity? product = null)
    {
        var p = product ?? Product();
        var ws = Domain.Warehouses.WarehouseStock.Create(Guid.NewGuid(), Guid.NewGuid(), p.Id, 5, 100).Value!;
        SetNav(ws, nameof(Domain.Warehouses.WarehouseStock.Product), p);
        return ws;
    }

    public static OrderDetail OrderDetail(ProductEntity? product = null)
    {
        var od = Domain.Orders.OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 2, 50m).Value!;
        if (product != null) SetNav(od, nameof(Domain.Orders.OrderDetail.Product), product);
        return od;
    }

    public static Order SaleOrder(CustomerEntity? customer = null, Warehouse? sourceWarehouse = null)
    {
        var details = new List<OrderDetail> { OrderDetail() };
        var o = Domain.Orders.Order.Create(Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.NewGuid(), null, "sale notes", 5m, details, DateTimeOffset.UtcNow.AddDays(7)).Value!;
        if (customer != null) SetNav(o, nameof(Order.Customer), customer);
        if (sourceWarehouse != null) SetNav(o, nameof(Order.SourceWarehouse), sourceWarehouse);
        return o;
    }

    public static InvoiceLineItem InvoiceLineItem(Guid? invoiceId = null)
        => Domain.Invoices.InvoiceLineItem.Create(1, invoiceId ?? Guid.NewGuid(), "Widget x2", 7.50m, 2, 50m).Value!;

    public static Invoice Invoice()
    {
        var id = Guid.NewGuid();
        var items = new List<InvoiceLineItem> { InvoiceLineItem(id) };
        return Domain.Invoices.Invoice.Create(id, InvoiceType.Sale, 5m, items, Guid.NewGuid()).Value!;
    }

    public static AdjustmentDetail AdjustmentDetail(ProductEntity? product = null)
    {
        var d = Domain.Adjustments.AdjustmentDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 10).Value!;
        if (product != null) SetNav(d, nameof(Domain.Adjustments.AdjustmentDetail.Product), product);
        return d;
    }

    public static Adjustment Adjustment(Warehouse? warehouse = null)
    {
        var details = new List<AdjustmentDetail> { AdjustmentDetail() };
        var adj = Domain.Adjustments.Adjustment.Create(Guid.NewGuid(), Guid.NewGuid(),
            AdjustmentReason.Damaged, details, AdjustmentType.Decrease, "damaged items").Value!;
        if (warehouse != null) SetNav(adj, nameof(Domain.Adjustments.Adjustment.Warehouse), warehouse);
        return adj;
    }
}
