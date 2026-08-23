using Contract.Common.Interfaces;
using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Orders;
using Domain.Suppliers;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Suppliers;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.CreateOrder;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreatePurchaseOrderTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreatePurchaseOrderTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<(Supplier Supplier, Domain.Warehouses.Warehouse Warehouse, Domain.Products.Product Product, Domain.Suppliers.SupplierProducts.SupplierProduct SupplierProduct)> SeedPurchaseGraphAsync(decimal purchasePrice = 5m, bool supplierActive = true)
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = Address.Create(Guid.NewGuid(), country.Id, city.Id, "Salah", "Mazen", $"Street-{unique}", "Valid address").Value;
        var category = CategoryFactory.CreateValid(name: $"Category-{unique}");
        var product = ProductFactory.CreateValid(sku:  $"{unique}", barCode: $"BAR-{unique}", productName: $"Product-{unique}", categoryId: category.Id, sellingPrice: 20m);
        var supplier = Supplier.Create(Guid.NewGuid(), $"Supplier-{unique}", $"SUP-{unique}", ContactInfoFactory.CreateValid(), address, supplierActive, "Valid supplier").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var warehouseStock = WarehouseStockFactory.CreateValid(warehouseId: warehouse.Id, productId: product.Id, quantity: 10m);
        var supplierProduct = SupplierProductFactory.CreateValid(Guid.NewGuid(), supplier.Id, product.Id, purchasePrice);

        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Addresses.AddAsync(address, CancellationToken.None);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.Suppliers.AddAsync(supplier, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(warehouseStock, CancellationToken.None);
        await _context.SupplierProducts.AddAsync(supplierProduct, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        supplierProduct = await _context.SupplierProducts.FirstAsync(x => x.SupplierId == supplier.Id && x.ProductId == product.Id, CancellationToken.None);
        return (supplier, warehouse, product, supplierProduct);
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var graph = await SeedPurchaseGraphAsync();
        var order = new CreateOrderCommand
        {
            SupplierId = graph.Supplier.Id,
            Discount = 10m,
            Notes = "valid order command",
            SourceWarehouseId = graph.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = graph.Product.Id, Quantity = 2m, RowVersion = graph.SupplierProduct.RowVersion }]
        };

        var result = await _mediator.Send(order, CancellationToken.None);
        _output.WriteLine(string.Join(", ", result.Errors.Select(e => e.Description + " " + e.Code)));

        Assert.True(result.IsSuccess);
        Assert.Equal((Guid?)graph.Supplier.Id, result.Value.SupplierId);
        Assert.Single(result.Value.OrderDetails);
    }

    [Fact]
    public async Task Handle_WithPastDueDate_ShouldFail()
    {
        var graph = await SeedPurchaseGraphAsync();
        var order = new CreateOrderCommand
        {
            SupplierId = graph.Supplier.Id,
            SourceWarehouseId = graph.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(-1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = graph.Product.Id, Quantity = 2m, RowVersion = graph.SupplierProduct.RowVersion }]
        };

        var result = await _mediator.Send(order, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WithWrongRowVersion_ShouldFail()
    {
        var graph = await SeedPurchaseGraphAsync();
        var order = new CreateOrderCommand
        {
            SupplierId = graph.Supplier.Id,
            SourceWarehouseId = graph.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = graph.Product.Id, Quantity = 2m, RowVersion = [7, 7, 7, 7] }]
        };

        var result = await _mediator.Send(order, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
