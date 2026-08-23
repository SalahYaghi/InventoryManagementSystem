using Contract.Common.Interfaces;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
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
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Contract.Features.Transactions.Order.Commands.UpdateOrderDetail;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.UpdateOrderDetailQuantity;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateOrderDetailQuantityCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateOrderDetailQuantityCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }


    private async Task<(Domain.Orders.Order Order, Domain.Products.Product Product, Domain.Warehouses.Warehouse Warehouse)> SeedPurchaseOrderEntityAsync(OrderStatus? status = null, decimal quantity = 2m, DateTimeOffset? dueDate = null)
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = Address.Create(Guid.NewGuid(), country.Id, city.Id, "Salah", "Mazen", $"Street-{unique}", "Valid address").Value;
        var category = CategoryFactory.CreateValid(name: $"Category-{unique}");
        var product = ProductFactory.CreateValid(sku:  $"{unique}", barCode: $"BAR-{unique}", productName: $"Product-{unique}", categoryId: category.Id, sellingPrice: 25m);
        var supplier = Supplier.Create(Guid.NewGuid(), $"Supplier-{unique}", $"SUP-{unique}", ContactInfoFactory.CreateValid(), address, true, "Valid supplier").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouse.Id, productId: product.Id, quantity: 10m);
        var detail = OrderDetailFactory.CreateValid(productId: product.Id, quantity: quantity, unitPrice: 5m);
        var order = OrderFactory.CreatePurchase(supplierId: supplier.Id, sourceWarehouseId: warehouse.Id, orderDetails: [detail], dueDate: dueDate ?? DateTimeOffset.UtcNow.AddDays(1));
        if (status.HasValue)
        {
            order.UpdateStatus(status.Value);
        }

        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Addresses.AddAsync(address, CancellationToken.None);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.Suppliers.AddAsync(supplier, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.Orders.AddAsync(order, CancellationToken.None);
        await _context.OrderDetails.AddAsync(detail, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        order = await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == order.Id, CancellationToken.None);
        return (order, product, warehouse);
    }

    private async Task<(Domain.Orders.Order Order, Domain.Products.Product Product, Domain.Warehouses.Warehouse Warehouse, Domain.Warehouses.WarehouseStock Stock)> SeedSaleOrderEntityAsync(decimal stockQuantity = 10m, decimal orderQuantity = 2m, OrderStatus? status = null)
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = Address.Create(Guid.NewGuid(), country.Id, city.Id, "Salah", "Mazen", $"Street-{unique}", "Valid address").Value;
        var category = CategoryFactory.CreateValid(name: $"Category-{unique}");
        var product = ProductFactory.CreateValid(sku:  $"{unique}", barCode: $"BAR-{unique}", productName: $"Product-{unique}", categoryId: category.Id, sellingPrice: 25m);
        var customer = Customer.Create(Guid.NewGuid(), $"Customer-{unique}", $"CUS-{unique}", ContactInfoFactory.CreateValid(), address, "Valid customer").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouse.Id, productId: product.Id, quantity: stockQuantity);
        var detail = OrderDetailFactory.CreateValid(productId: product.Id, quantity: orderQuantity, unitPrice: 25m);
        var order = OrderFactory.CreateSale(customerId: customer.Id, sourceWarehouseId: warehouse.Id, orderDetails: [detail], dueDate: DateTimeOffset.UtcNow.AddDays(1));
        if (status.HasValue)
        {
            order.UpdateStatus(status.Value);
        }

        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Addresses.AddAsync(address, CancellationToken.None);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.Customers.AddAsync(customer, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.Orders.AddAsync(order, CancellationToken.None);
        await _context.OrderDetails.AddAsync(detail, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        order = await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == order.Id, CancellationToken.None);
        stock = await _context.WarehouseStocks.FirstAsync(x => x.WarehouseId == warehouse.Id && x.ProductId == product.Id, CancellationToken.None);
        return (order, product, warehouse, stock);
    }


    [Fact]
    public async Task Handle_WithValidPurchaseDetailQuantity_ShouldSucceedAndPersistQuantity()
    {
        var graph = await SeedPurchaseOrderEntityAsync(quantity: 2m);
        var detail = await _context.OrderDetails.FirstAsync(x => x.OrderId == graph.Order.Id, CancellationToken.None);
        var command = new UpdateOrderDetailCommand { Id = detail.Id, Quantity = 4m, RowVersion = detail.RowVersion };

        var result = await _mediator.Send(command, CancellationToken.None);
        _context.ClearChangeTracker();

        Assert.True(result.IsSuccess, string.Join(", ", result.Errors.Select(e => e.Code)));
        var updated = await _context.OrderDetails.FirstAsync(x => x.Id == detail.Id, CancellationToken.None);
        Assert.Equal(4m, updated.Quantity);
    }

    [Fact]
    public async Task Handle_WithSameQuantity_ShouldReturnSuccessWithoutChangingQuantity()
    {
        var graph = await SeedPurchaseOrderEntityAsync(quantity: 2m);
        var detail = await _context.OrderDetails.FirstAsync(x => x.OrderId == graph.Order.Id, CancellationToken.None);
        var command = new UpdateOrderDetailCommand { Id = detail.Id, Quantity = detail.Quantity, RowVersion = detail.RowVersion };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WithMissingDetail_ShouldFail()
    {
        var command = new UpdateOrderDetailCommand { Id = Guid.NewGuid(), Quantity = 2m, RowVersion = [1, 2, 3, 4] };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithWrongRowVersion_ShouldFail()
    {
        var graph = await SeedPurchaseOrderEntityAsync();
        var detail = await _context.OrderDetails.FirstAsync(x => x.OrderId == graph.Order.Id, CancellationToken.None);
        var command = new UpdateOrderDetailCommand { Id = detail.Id, Quantity = 3m, RowVersion = [9, 9, 9, 9] };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithZeroQuantity_ShouldFailFromDomain()
    {
        var graph = await SeedPurchaseOrderEntityAsync();
        var detail = await _context.OrderDetails.FirstAsync(x => x.OrderId == graph.Order.Id, CancellationToken.None);
        var command = new UpdateOrderDetailCommand { Id = detail.Id, Quantity = 0m, RowVersion = detail.RowVersion };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithCompletedOrder_ShouldFailBecauseOrderIsLocked()
    {
        var graph = await SeedPurchaseOrderEntityAsync(status: OrderStatus.Completed);
        var detail = await _context.OrderDetails.FirstAsync(x => x.OrderId == graph.Order.Id, CancellationToken.None);
        var command = new UpdateOrderDetailCommand { Id = detail.Id, Quantity = 3m, RowVersion = detail.RowVersion };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithSaleQuantityGreaterThanAvailableStock_ShouldFail()
    {
        var graph = await SeedSaleOrderEntityAsync(stockQuantity: 2m, orderQuantity: 1m);
        var detail = await _context.OrderDetails.FirstAsync(x => x.OrderId == graph.Order.Id, CancellationToken.None);
        var command = new UpdateOrderDetailCommand { Id = detail.Id, Quantity = 100m, RowVersion = detail.RowVersion };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
    }
}
