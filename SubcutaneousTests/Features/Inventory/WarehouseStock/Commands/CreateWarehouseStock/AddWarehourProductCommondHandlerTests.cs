using Contract.Common.Interfaces;
using Contract.Features.Inventory.Product.Commands.CreateProduct;
using Contract.Features.Inventory.WarehouseStock.Commands.AddWarehouseProducts;
using Contract.Features.Inventory.WarehouseStocks.Commands.DeleteWarehouseStock;
using Contract.Features.Inventory.WarehouseStocks.Commands.UpdateWarehouseStock;
using Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged;
using Domain.Contacts.Address.Country;
using Domain.Products.Enums;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Inventory.WarehouseStock.Commands.CreateWarehouseStock;

[Collection(WebAppFactoryCollection.CollectionName)]
public class AddWarehourProductCommondHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public AddWarehourProductCommondHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<Guid> SeedWarehouseAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = AddressFactory.CreateValid(countryId: country.Id, cityId: city.Id);
        var warehouse = WarehouseFactory.CreateValid(name: $"Warehouse-{unique}", code: $"WH-{unique}", address: address);
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return warehouse.Id;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldCreateProductAndWarehouseStock()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var warehouseId = await SeedWarehouseAsync();
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new AddWarehourProductCommand
        {
            WarehousesId = warehouseId,
            Product = new CreateProductCommand
            {
                SKU = $"S{unique}",
                BarCode = $"BAR-{unique}",
                ProductName = $"Product-{unique}",
                Description = "Created through warehouse stock flow",
                SellingPrice = 25m,
                IsActive = true,
                Unit = Domain.Products.Enums.Unit.Piece,
                CategoryId = category.Id
            }
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(warehouseId, result.Value.WarehouseId);
        Assert.True(await _context.Products.AnyAsync(x => x.Id == result.Value.ProductId, CancellationToken.None));
        Assert.True(await _context.WarehouseStocks.AnyAsync(x => x.Id == result.Value.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithMissingWarehouse_ShouldFail()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new AddWarehourProductCommand
        {
            WarehousesId = Guid.NewGuid(),
            Product = new CreateProductCommand
            {
                SKU = $"S{unique}",
                BarCode = $"BAR-{unique}",
                ProductName = $"Product-{unique}",
                SellingPrice = 25m,
                IsActive = true,
                Unit = Domain.Products.Enums.Unit.Piece,
                CategoryId = category.Id
            }
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal("Warehouse.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Handle_WithDuplicateProductSku_ShouldFail()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var warehouseId = await SeedWarehouseAsync();
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new AddWarehourProductCommand
        {
            WarehousesId = warehouseId,
            Product = new CreateProductCommand
            {
                SKU = product.SKU,
                BarCode = $"BAR-DUP-{unique}",
                ProductName = $"Product-Dup-{unique}",
                SellingPrice = 25m,
                IsActive = true,
                Unit = Domain.Products.Enums.Unit.Piece,
                CategoryId = category.Id
            }
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task UpdateMinimumLevel_WithExistingStock_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var warehouseId = await SeedWarehouseAsync();
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouseId, productId: product.Id, minimumStockLevel: 5m, quantity: 20m);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new UpdateWarehouseStockMinimumLevelCommand { Id = stock.Id, MinimumStockLevel = 12m }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(12m, result.Value.MinimumStockLevel);
    }

    [Fact]
    public async Task UpdateMinimumLevel_WithMissingStock_ShouldFail()
    {
        var result = await _mediator.Send(new UpdateWarehouseStockMinimumLevelCommand { Id = Guid.NewGuid(), MinimumStockLevel = 12m }, CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("WarehouseStock.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task UpdateMinimumLevel_WithNegativeMinimum_ShouldFail()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var warehouseId = await SeedWarehouseAsync();
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouseId, productId: product.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new UpdateWarehouseStockMinimumLevelCommand { Id = stock.Id, MinimumStockLevel = -1m }, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Delete_WithExistingStock_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var warehouseId = await SeedWarehouseAsync();
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouseId, productId: product.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new DeleteWarehouseStockCommand(stock.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.False(await _context.WarehouseStocks.AnyAsync(x => x.Id == stock.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Delete_WithMissingStock_ShouldFail()
    {
        var result = await _mediator.Send(new DeleteWarehouseStockCommand(Guid.NewGuid()), CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("WarehouseStock.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task GetPaged_WithWarehouseStock_ShouldReturnData()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var warehouseId = await SeedWarehouseAsync();
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouseId, productId: product.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new GetWarehouseStockPagedQuery(warehouseId) { PageNumber = 1, PageSize = 10 }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Contains(result.Value.Items, x => x.Id == stock.Id);
    }
}
