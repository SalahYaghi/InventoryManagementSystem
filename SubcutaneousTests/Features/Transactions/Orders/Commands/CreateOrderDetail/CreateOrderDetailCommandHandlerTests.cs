using Contract.Common.Interfaces;
using Contract.Features.Transactions.Order.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Customer;
using Domain.Orders;
using Domain.Suppliers;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Orders;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Suppliers;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;
using Contract.Features.Transactions.Orders.DTOs;
using Domain.Warehouses;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.CreateOrderDetail;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateOrderDetailCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateOrderDetailCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }


    private async Task<(Domain.Products.Product Product, Domain.Products.Category.Category Category)> SeedProductAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Category-{unique}");
        var product = ProductFactory.CreateValid(sku:  $"{unique}", barCode: $"BAR-{unique}", productName: $"Product-{unique}", categoryId: category.Id, sellingPrice: 30m);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        return (product, category);
    }

    private async Task<Address> SeedAddressAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = Address.Create(Guid.NewGuid(), country.Id, city.Id, "Salah", "Mazen", $"Street-{unique}", "Valid address").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Addresses.AddAsync(address, CancellationToken.None);
        return address;
    }

    private async Task<(Domain.Orders.Order Order, Domain.Products.Product Product, Domain.Suppliers.SupplierProducts.SupplierProduct SupplierProduct)> SeedPurchaseOrderWithoutProductDetailAsync()
    {
        var address = await SeedAddressAsync();
        var (existingProduct, _) = await SeedProductAsync();
        var (newProduct, _) = await SeedProductAsync();
        var unique = Guid.NewGuid().ToString("N")[..8];
        var supplier = Supplier.Create(Guid.NewGuid(), $"Supplier-{unique}", $"SUP-{unique}", ContactInfoFactory.CreateValid(), address, true, "Valid supplier").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var existingDetail = OrderDetailFactory.CreateValid(productId: existingProduct.Id, quantity: 1m, unitPrice: 5m);
        var order = OrderFactory.CreatePurchase(supplierId: supplier.Id, sourceWarehouseId: warehouse.Id, orderDetails: [existingDetail], dueDate: DateTimeOffset.UtcNow.AddDays(1));
        var supplierProduct = SupplierProductFactory.CreateValid(Guid.NewGuid(), supplier.Id, newProduct.Id, 7m);

        await _context.Suppliers.AddAsync(supplier, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.Orders.AddAsync(order, CancellationToken.None);
        await _context.OrderDetails.AddAsync(existingDetail, CancellationToken.None);
        await _context.SupplierProducts.AddAsync(supplierProduct, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        supplierProduct = await _context.SupplierProducts.FirstAsync(x => x.SupplierId == supplier.Id && x.ProductId == newProduct.Id, CancellationToken.None);
        order = await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == order.Id, CancellationToken.None);
        return (order, newProduct, supplierProduct);
    }

    private async Task<(Domain.Orders.Order Order, Domain.Products.Product Product, Domain.Warehouses.WarehouseStock Stock)> SeedSaleOrderWithoutProductDetailAsync(decimal stockQuantity = 10m)
    {
        var address = await SeedAddressAsync();
        var (existingProduct, _) = await SeedProductAsync();
        var (newProduct, _) = await SeedProductAsync();
        var unique = Guid.NewGuid().ToString("N")[..8];
        var customer = Customer.Create(Guid.NewGuid(), $"Customer-{unique}", $"CUS-{unique}", ContactInfoFactory.CreateValid(), address, "Valid customer").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var existingDetail = OrderDetailFactory.CreateValid(productId: existingProduct.Id, quantity: 1m, unitPrice: 30m);
        var order = OrderFactory.CreateSale(customerId: customer.Id, sourceWarehouseId: warehouse.Id, orderDetails: [existingDetail], dueDate: DateTimeOffset.UtcNow.AddDays(1));
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouse.Id, productId: newProduct.Id, quantity: stockQuantity);

        await _context.Customers.AddAsync(customer, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.Orders.AddAsync(order, CancellationToken.None);
        await _context.OrderDetails.AddAsync(existingDetail, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        stock = await _context.WarehouseStocks.FirstAsync(x => x.WarehouseId == warehouse.Id && x.ProductId == newProduct.Id, CancellationToken.None);
        order = await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == order.Id, CancellationToken.None);
        return (order, newProduct, stock);
    }


    [Fact]
    public async Task Handle_WithValidPurchaseDetail_ShouldSucceedAndPersistDetail()
    {
        var graph = await SeedPurchaseOrderWithoutProductDetailAsync();
        var command = new CreateOrderDetailCommand { OrderId = graph.Order.Id, ProductId = graph.Product.Id, Quantity = 2m, RowVersion = graph.SupplierProduct.RowVersion };

        var result = await _mediator.Send(command, CancellationToken.None);
        _output.WriteLine(string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description)));

        Assert.True(result.IsSuccess);
        Assert.True(await _context.OrderDetails.AnyAsync(x => x.Id == result.Value.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithValidSaleDetail_ShouldSucceedAndPersistDetail()
    {
        var graph = await SeedSaleOrderWithoutProductDetailAsync(stockQuantity: 20m);
        var command = new CreateOrderDetailCommand { OrderId = graph.Order.Id, ProductId = graph.Product.Id, Quantity = 2m, RowVersion = graph.Stock.RowVersion };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess, string.Join(", ", result.Errors.Select(e => e.Code)));
        Assert.Equal(graph.Product.Id, result.Value.ProductId);
    }

    [Fact]
    public async Task Handle_WithMissingOrder_ShouldFail()
    {
        var graph = await SeedPurchaseOrderWithoutProductDetailAsync();
        var command = new CreateOrderDetailCommand { OrderId = Guid.NewGuid(), ProductId = graph.Product.Id, Quantity = 2m, RowVersion = graph.SupplierProduct.RowVersion };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithDuplicateProductInOrder_ShouldFail()
    {
        var graph = await SeedPurchaseOrderWithoutProductDetailAsync();
        var existingDetail = graph.Order.OrderDetails.First();
        var command = new CreateOrderDetailCommand { OrderId = graph.Order.Id, ProductId = existingDetail.ProductId, Quantity = 2m, RowVersion = graph.SupplierProduct.RowVersion };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithWrongSupplierProductRowVersion_ShouldFail()
    {
        var graph = await SeedPurchaseOrderWithoutProductDetailAsync();
        var command = new CreateOrderDetailCommand { OrderId = graph.Order.Id, ProductId = graph.Product.Id, Quantity = 2m, RowVersion = [9, 9, 9, 9] };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithSaleQuantityGreaterThanStock_ShouldFail()
    {
        var graph = await SeedSaleOrderWithoutProductDetailAsync(stockQuantity: 1m);
        var command = new CreateOrderDetailCommand { OrderId = graph.Order.Id, ProductId = graph.Product.Id, Quantity = 100m, RowVersion = graph.Stock.RowVersion };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }
}

