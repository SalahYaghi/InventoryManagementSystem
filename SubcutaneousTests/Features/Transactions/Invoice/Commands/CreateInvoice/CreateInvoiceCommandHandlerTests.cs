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

using Contract.Features.Transactions.Invoice.Commands.CreateInvoice;
using Contract.Features.Transactions.Invoice.Queries.GetInvoice;

namespace SubcutaneousTests.Features.Transactions.Invoice.Commands.CreateInvoice;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateInvoiceCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateInvoiceCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }


    private async Task<(Domain.Orders.Order Order, Domain.Products.Product Product)> SeedCompletedPurchaseOrderAsync(bool completed = true)
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = Address.Create(Guid.NewGuid(), country.Id, city.Id, "Salah", "Mazen", $"Street-{unique}", "Valid address").Value;
        var category = CategoryFactory.CreateValid(name: $"Category-{unique}");
        var product = ProductFactory.CreateValid(sku:  $"{unique}", barCode: $"BAR-{unique}", productName: $"Product-{unique}", categoryId: category.Id, sellingPrice: 25m);
        var supplier = Supplier.Create(Guid.NewGuid(), $"Supplier-{unique}", $"SUP-{unique}", ContactInfoFactory.CreateValid(), address, true, "Valid supplier").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var detail = OrderDetailFactory.CreateValid(productId: product.Id, quantity: 2m, unitPrice: 5m);
        var order = OrderFactory.CreatePurchase(supplierId: supplier.Id, sourceWarehouseId: warehouse.Id, orderDetails: [detail], dueDate: DateTimeOffset.UtcNow.AddDays(1));
        if (completed)
        {
            order.UpdateStatus(OrderStatus.Completed);
        }

        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Addresses.AddAsync(address, CancellationToken.None);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.Suppliers.AddAsync(supplier, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.Orders.AddAsync(order, CancellationToken.None);
        await _context.OrderDetails.AddAsync(detail, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        order = await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == order.Id, CancellationToken.None);
        return (order, product);
    }

    private async Task<Domain.Orders.Order> SeedCompletedTransferOrderAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = Address.Create(Guid.NewGuid(), country.Id, city.Id, "Salah", "Mazen", $"Street-{unique}", "Valid address").Value;
        var category = CategoryFactory.CreateValid(name: $"Category-{unique}");
        var product = ProductFactory.CreateValid(sku:  $"{unique}", barCode: $"BAR-{unique}", productName: $"Product-{unique}", categoryId: category.Id, sellingPrice: 25m);
        var source = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Source-{unique}", $"SRC-{unique}", address);
        var destination = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Destination-{unique}", $"DST-{unique}", address);
        var detail = OrderDetailFactory.CreateValid(productId: product.Id, quantity: 2m, unitPrice: 25m);
        var order = OrderFactory.CreateTransfer(sourceWarehouseId: source.Id, destinationWarehouseId: destination.Id, orderDetails: [detail], dueDate: DateTimeOffset.UtcNow.AddDays(1));
        order.UpdateStatus(OrderStatus.Completed);

        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Addresses.AddAsync(address, CancellationToken.None);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.Warehouses.AddAsync(source, CancellationToken.None);
        await _context.Warehouses.AddAsync(destination, CancellationToken.None);
        await _context.Orders.AddAsync(order, CancellationToken.None);
        await _context.OrderDetails.AddAsync(detail, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        return await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == order.Id, CancellationToken.None);
    }


    [Fact]
    public async Task Handle_WithCompletedPurchaseOrder_ShouldSucceedAndPersistInvoice()
    {
        var graph = await SeedCompletedPurchaseOrderAsync(completed: true);
        var command = new CreateInvoiceCommand { OrderId = graph.Order.Id };

        var result = await _mediator.Send(command, CancellationToken.None);
        _output.WriteLine(string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description)));

        Assert.True(result.IsSuccess);
        Assert.Equal(graph.Order.Id, result.Value.OrderId);
        Assert.Single(result.Value.InvoiceLineItems);
        Assert.True(await _context.Invoices.AnyAsync(x => x.Id == result.Value.InvoiceId, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithMissingOrder_ShouldFail()
    {
        var result = await _mediator.Send(new CreateInvoiceCommand { OrderId = Guid.NewGuid() }, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithPendingOrder_ShouldFailBecauseOrderIsNotCompleted()
    {
        var graph = await SeedCompletedPurchaseOrderAsync(completed: false);
        var result = await _mediator.Send(new CreateInvoiceCommand { OrderId = graph.Order.Id }, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithTransferOrder_ShouldFailBecauseInvoiceTypeIsNotSupported()
    {
        var order = await SeedCompletedTransferOrderAsync();
        var result = await _mediator.Send(new CreateInvoiceCommand { OrderId = order.Id }, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WhenOrderAlreadyHasInvoice_ShouldFail()
    {
        var graph = await SeedCompletedPurchaseOrderAsync(completed: true);
        var first = await _mediator.Send(new CreateInvoiceCommand { OrderId = graph.Order.Id }, CancellationToken.None);
        Assert.True(first.IsSuccess);

        var second = await _mediator.Send(new CreateInvoiceCommand { OrderId = graph.Order.Id }, CancellationToken.None);
        Assert.True(second.IsError);
    }
}
