using Application.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.WarehouseStocks.Commands.DeleteWarehouseStock;
using Domain.Contacts.Address.Country;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetWarehouseStockByIdQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetWarehouseStockByIdQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
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

    private async Task<Domain.Warehouses.WarehouseStock> SeedWarehouseStockAsync(
        decimal minimumStockLevel = 5m, decimal quantity = 20m)
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var warehouseId = await SeedWarehouseAsync();
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        var stock = WarehouseStockFactory.CreateValid(
            warehouseId: warehouseId,
            productId: product.Id,
            minimumStockLevel: minimumStockLevel,
            quantity: quantity);

        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        return stock;
    }

    [Fact]
    public async Task Handle_WithExistingStock_ShouldReturnStock()
    {
        var stock = await SeedWarehouseStockAsync();

        var result = await _mediator.Send(new GetWarehouseStockByIdQuery(stock.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(stock.Id, result.Value.Id);
    }

    [Fact]
    public async Task Handle_WithExistingStock_ShouldMapAllFields()
    {
        var stock = await SeedWarehouseStockAsync(minimumStockLevel: 7m, quantity: 42m);

        var result = await _mediator.Send(new GetWarehouseStockByIdQuery(stock.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(stock.WarehouseId, result.Value.WarehouseId);
        Assert.Equal(stock.ProductId, result.Value.ProductId);
        Assert.Equal(42m, result.Value.Quantity);
        Assert.Equal(7m, result.Value.MinimumStockLevel);
    }

    [Fact]
    public async Task Handle_WithExistingStock_ShouldReturnRowVersion()
    {
        var stock = await SeedWarehouseStockAsync();

        var result = await _mediator.Send(new GetWarehouseStockByIdQuery(stock.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value.RowVersion);
    }

    [Fact]
    public async Task Handle_WithMissingStock_ShouldFail()
    {
        var result = await _mediator.Send(new GetWarehouseStockByIdQuery(Guid.NewGuid()), CancellationToken.None);

        Assert.True(result.IsError);
        _output.WriteLine($"Error code: {result.TopError.Code}");
        Assert.Equal("Product.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Handle_WithEmptyId_ShouldFail()
    {
        var result = await _mediator.Send(new GetWarehouseStockByIdQuery(Guid.Empty), CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_AfterStockIsDeleted_ShouldFail()
    {
        var stock = await SeedWarehouseStockAsync();

        var deleted = await _mediator.Send(new DeleteWarehouseStockCommand(stock.Id), CancellationToken.None);
        Assert.True(deleted.IsSuccess);

        var result = await _mediator.Send(new GetWarehouseStockByIdQuery(stock.Id), CancellationToken.None);

        Assert.True(result.IsError);
    }
}
