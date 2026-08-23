using Contract.Common.Interfaces;
using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Customer;
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
public class CreateOrderCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateOrderCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }


    private async Task<(Country Country, City City, Address Address)> SeedAddressGraphAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = Address.Create(Guid.NewGuid(), country.Id, city.Id, "Salah", "Mazen", $"Street-{unique}", "Valid address").Value;

        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Addresses.AddAsync(address, CancellationToken.None);

        return (country, city, address);
    }

    private async Task<(Domain.Products.Category.Category Category, Domain.Products.Product Product)> SeedProductAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Category-{unique}");
        var product = ProductFactory.CreateValid(
            sku:  $"{unique}",
            barCode: $"BAR-{unique}",
            productName: $"Product-{unique}",
            categoryId: category.Id,
            sellingPrice: 20m);

        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);

        return (category, product);
    }

    private async Task<(Supplier Supplier, Domain.Warehouses.Warehouse Warehouse, Domain.Products.Product Product, Domain.Suppliers.SupplierProducts.SupplierProduct SupplierProduct, Domain.Warehouses.WarehouseStock WarehouseStock)> SeedPurchaseWorldAsync(decimal stockQuantity = 10m, decimal purchasePrice = 5m, bool supplierActive = true)
    {
        var (_, _, address) = await SeedAddressGraphAsync();
        var (_, product) = await SeedProductAsync();
        var unique = Guid.NewGuid().ToString("N")[..8];

        var supplier = Supplier.Create(Guid.NewGuid(), $"Supplier-{unique}", $"SUP-{unique}", ContactInfoFactory.CreateValid(), address, supplierActive, "Valid supplier").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouse.Id, productId: product.Id, quantity: stockQuantity);
        var supplierProduct = SupplierProductFactory.CreateValid(Guid.NewGuid(), supplier.Id, product.Id, purchasePrice);

        await _context.Suppliers.AddAsync(supplier, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SupplierProducts.AddAsync(supplierProduct, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var savedSupplierProduct = await _context.SupplierProducts.FirstAsync(x => x.SupplierId == supplier.Id && x.ProductId == product.Id, CancellationToken.None);
        var savedStock = await _context.WarehouseStocks.FirstAsync(x => x.WarehouseId == warehouse.Id && x.ProductId == product.Id, CancellationToken.None);

        return (supplier, warehouse, product, savedSupplierProduct, savedStock);
    }

    private async Task<(Customer Customer, Domain.Warehouses.Warehouse Warehouse, Domain.Products.Product Product, Domain.Warehouses.WarehouseStock WarehouseStock)> SeedSaleWorldAsync(decimal stockQuantity = 10m)
    {
        var (_, _, address) = await SeedAddressGraphAsync();
        var (_, product) = await SeedProductAsync();
        var unique = Guid.NewGuid().ToString("N")[..8];

        var customer = Customer.Create(Guid.NewGuid(), $"Customer-{unique}", $"CUS-{unique}", ContactInfoFactory.CreateValid(), address, "Valid customer").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouse.Id, productId: product.Id, quantity: stockQuantity);

        await _context.Customers.AddAsync(customer, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var savedStock = await _context.WarehouseStocks.FirstAsync(x => x.WarehouseId == warehouse.Id && x.ProductId == product.Id, CancellationToken.None);

        return (customer, warehouse, product, savedStock);
    }

    private async Task<(Domain.Warehouses.Warehouse SourceWarehouse, Domain.Warehouses.Warehouse DestinationWarehouse, Domain.Products.Product Product, Domain.Warehouses.WarehouseStock SourceStock)> SeedTransferWorldAsync(decimal sourceQuantity = 10m)
    {
        var (_, _, address) = await SeedAddressGraphAsync();
        var (_, product) = await SeedProductAsync();
        var unique = Guid.NewGuid().ToString("N")[..8];

        var sourceWarehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Source-{unique}", $"SRC-{unique}", address);
        var destinationWarehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Destination-{unique}", $"DST-{unique}", address);
        var sourceStock = WarehouseStockFactory.CreateValid(warehouseId: sourceWarehouse.Id, productId: product.Id, quantity: sourceQuantity);

        await _context.Warehouses.AddAsync(sourceWarehouse, CancellationToken.None);
        await _context.Warehouses.AddAsync(destinationWarehouse, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(sourceStock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var savedSourceStock = await _context.WarehouseStocks.FirstAsync(x => x.WarehouseId == sourceWarehouse.Id && x.ProductId == product.Id, CancellationToken.None);
        return (sourceWarehouse, destinationWarehouse, product, savedSourceStock);
    }

    private async Task<Domain.Orders.Order> CreatePersistedPurchaseOrderAsync(decimal quantity = 2m, decimal stockQuantity = 10m, DateTimeOffset? dueDate = null)
    {
        var world = await SeedPurchaseWorldAsync(stockQuantity: stockQuantity);
        var command = new CreateOrderCommand
        {
            SupplierId = world.Supplier.Id,
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = dueDate ?? DateTimeOffset.UtcNow.AddDays(1),
            Notes = "Valid purchase order",
            Discount = 0m,
            OrderDetails =
            [
                new CreateOrderDetailCommand
                {
                    ProductId = world.Product.Id,
                    Quantity = quantity,
                    RowVersion = world.SupplierProduct.RowVersion
                }
            ]
        };

        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsSuccess, string.Join(", ", result.Errors.Select(e => e.Code + ": " + e.Description)));

        return await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == result.Value.Id, CancellationToken.None);
    }

    private async Task<Domain.Orders.Order> CreatePersistedSaleOrderAsync(decimal quantity = 2m, decimal stockQuantity = 10m, DateTimeOffset? dueDate = null)
    {
        var world = await SeedSaleWorldAsync(stockQuantity: stockQuantity);
        var command = new CreateOrderCommand
        {
            CustomerId = world.Customer.Id,
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Sale,
            DueDate = dueDate ?? DateTimeOffset.UtcNow.AddDays(1),
            Notes = "Valid sale order",
            Discount = 0m,
            OrderDetails =
            [
                new CreateOrderDetailCommand
                {
                    ProductId = world.Product.Id,
                    Quantity = quantity,
                    RowVersion = world.WarehouseStock.RowVersion
                }
            ]
        };

        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsSuccess, string.Join(", ", result.Errors.Select(e => e.Code + ": " + e.Description)));

        return await _context.Orders.Include(x => x.OrderDetails).FirstAsync(x => x.Id == result.Value.Id, CancellationToken.None);
    }


    [Fact]
    public async Task Handle_WithValidPurchaseOrder_ShouldSucceedAndPersistOrder()
    {
        var world = await SeedPurchaseWorldAsync();

        var command = new CreateOrderCommand
        {
            SupplierId = world.Supplier.Id,
            Discount = 1m,
            Notes = "valid purchase order",
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 2m, RowVersion = world.SupplierProduct.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);
        _output.WriteLine(string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description)));

        Assert.True(result.IsSuccess);
        Assert.Equal((Guid?)world.Supplier.Id, result.Value.SupplierId);
        Assert.Equal((Guid?)world.Warehouse.Id, result.Value.SourceWarehouseId);
        Assert.Single(result.Value.OrderDetails);
        Assert.True(await _context.Orders.AnyAsync(x => x.Id == result.Value.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithValidSaleOrder_ShouldSucceedAndPersistOrder()
    {
        var world = await SeedSaleWorldAsync(stockQuantity: 20m);
        var command = new CreateOrderCommand
        {
            CustomerId = world.Customer.Id,
            Discount = 0m,
            Notes = "valid sale order",
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Sale,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 2m, RowVersion = world.WarehouseStock.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal((Guid?)world.Customer.Id, result.Value.CustomerId);
        Assert.Equal(OrderType.Sale, result.Value.OrderType);
        Assert.Single(result.Value.OrderDetails);
    }

    [Fact]
    public async Task Handle_WithValidTransferOrder_ShouldSucceed()
    {
        var world = await SeedTransferWorldAsync(sourceQuantity: 20m);
        var command = new CreateOrderCommand
        {
            SourceWarehouseId = world.SourceWarehouse.Id,
            DestinationWarehouseId = world.DestinationWarehouse.Id,
            OrderType = OrderType.Transfer,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            Notes = "valid transfer order",
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 3m, RowVersion = world.SourceStock.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess, string.Join(", ", result.Errors.Select(e => e.Code)));
        Assert.Equal((Guid?)world.SourceWarehouse.Id, result.Value.SourceWarehouseId);
        Assert.Equal((Guid?)world.DestinationWarehouse.Id, result.Value.DestinationWarehouseId);
    }

    [Fact]
    public async Task Handle_WithPastDueDate_ShouldFailBeforeSavingOrder()
    {
        var world = await SeedPurchaseWorldAsync();
        var command = new CreateOrderCommand
        {
            SupplierId = world.Supplier.Id,
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(-1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 2m, RowVersion = world.SupplierProduct.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.False(await _context.Orders.AnyAsync(x => x.SupplierId == world.Supplier.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithMissingSupplierProduct_ShouldFailWithProductNotFound()
    {
        var (_, _, address) = await SeedAddressGraphAsync();
        var (_, product) = await SeedProductAsync();
        var unique = Guid.NewGuid().ToString("N")[..8];
        var supplier = Supplier.Create(Guid.NewGuid(), $"Supplier-{unique}", $"SUP-{unique}", ContactInfoFactory.CreateValid(), address, true, "Valid").Value;
        var warehouse = WarehouseFactory.CreateValid(Guid.NewGuid(), $"Warehouse-{unique}", $"WH-{unique}", address);
        await _context.Suppliers.AddAsync(supplier, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new CreateOrderCommand
        {
            SupplierId = supplier.Id,
            SourceWarehouseId = warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = product.Id, Quantity = 2m, RowVersion = [1, 2, 3, 4] }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithStaleSupplierProductRowVersion_ShouldFail()
    {
        var world = await SeedPurchaseWorldAsync();
        var command = new CreateOrderCommand
        {
            SupplierId = world.Supplier.Id,
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 2m, RowVersion = [9, 9, 9, 9] }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithInactiveSupplier_ShouldFail()
    {
        var world = await SeedPurchaseWorldAsync(supplierActive: false);
        var command = new CreateOrderCommand
        {
            SupplierId = world.Supplier.Id,
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 2m, RowVersion = world.SupplierProduct.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithSaleQuantityGreaterThanWarehouseStock_ShouldFail()
    {
        var world = await SeedSaleWorldAsync(stockQuantity: 1m);
        var command = new CreateOrderCommand
        {
            CustomerId = world.Customer.Id,
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Sale,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 300m, RowVersion = world.WarehouseStock.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithMissingSourceWarehouse_ShouldFail()
    {
        var world = await SeedPurchaseWorldAsync();
        var command = new CreateOrderCommand
        {
            SupplierId = world.Supplier.Id,
            SourceWarehouseId = Guid.NewGuid(),
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 2m, RowVersion = world.SupplierProduct.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithMissingDestinationWarehouseForTransfer_ShouldFail()
    {
        var world = await SeedTransferWorldAsync();
        var command = new CreateOrderCommand
        {
            SourceWarehouseId = world.SourceWarehouse.Id,
            DestinationWarehouseId = Guid.NewGuid(),
            OrderType = OrderType.Transfer,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 1m, RowVersion = world.SourceStock.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithDiscountLargerThanSubtotal_ShouldFail()
    {
        var world = await SeedPurchaseWorldAsync(purchasePrice: 5m);
        var command = new CreateOrderCommand
        {
            SupplierId = world.Supplier.Id,
            SourceWarehouseId = world.Warehouse.Id,
            OrderType = OrderType.Purchase,
            Discount = 1000m,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = [new CreateOrderDetailCommand { ProductId = world.Product.Id, Quantity = 1m, RowVersion = world.SupplierProduct.RowVersion }]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }
}
