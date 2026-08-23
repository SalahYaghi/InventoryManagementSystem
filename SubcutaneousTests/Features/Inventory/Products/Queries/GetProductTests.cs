using Contract.Common.Interfaces;
using Contract.Features.Inventory.Product.Commands.CreateProduct;
using Contract.Features.Inventory.Product.Commands.DeleteProduct;
using Contract.Features.Inventory.Product.Commands.UpdateProduct;
using Contract.Features.Inventory.Product.Queries.GetProduct;
using Contract.Features.Inventory.Product.Queries.GetProductPaged;
using Domain.Products.Enums;
using InventoryManagement.Tests.Common.Factories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Inventory.Products.Queries;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetProductTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetProductTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new CreateProductCommand
        {
            SKU = $"S{unique}",
            BarCode = $"BAR-{unique}",
            ProductName = $"Product-{unique}",
            Description = "Valid product",
            SellingPrice = 25m,
            IsActive = true,
            Unit = Domain.Products.Enums.Unit.Piece,
            CategoryId = category.Id
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.SKU, result.Value.SKU);
        Assert.Equal(command.ProductName, result.Value.ProductName);
        Assert.Equal(category.Id, result.Value.CategoryId);
        Assert.True(await _context.Products.AnyAsync(x => x.Id == result.Value.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithDuplicateSku_ShouldFail()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var existing = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(existing, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new CreateProductCommand
        {
            SKU = existing.SKU,
            BarCode = $"BAR-NEW-{unique}",
            ProductName = $"Product-New-{unique}",
            Description = "Duplicate SKU test",
            SellingPrice = 30m,
            IsActive = true,
            Unit = Domain.Products.Enums.Unit.Box,
            CategoryId = category.Id
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithEmptySku_ShouldFail()
    {
        var category = CategoryFactory.CreateValid(name: $"Cat-{Guid.NewGuid().ToString("N")[..8]}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new CreateProductCommand
        {
            SKU = string.Empty,
            BarCode = "BAR-EMPTY",
            ProductName = "Product",
            SellingPrice = 10m,
            IsActive = true,
            Unit = Domain.Products.Enums.Unit.Piece,
            CategoryId = category.Id
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Update_WithExistingProduct_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new UpdateProductCommand
        {
            Id = product.Id,
            SKU = $"U{unique}",
            BarCode = $"BAR-U-{unique}",
            ProductName = $"Updated-{unique}",
            Description = "Updated description",
            SellingPrice = 55m,
            IsActive = false,
               Unit = Domain.Products.Enums.Unit.Kg,
            CategoryId = category.Id
        };

        var result = await _mediator.Send(command, CancellationToken.None);
        _context.ClearChangeTracker();

        Assert.True(result.IsSuccess);
        Assert.Equal(command.SKU, result.Value.SKU);
        Assert.Equal(command.ProductName, result.Value.ProductName);
        Assert.Equal(command.SellingPrice, result.Value.SellingPrice);

        var productFromDb = await _context.Products.FirstAsync(x => x.Id == product.Id, CancellationToken.None);
        Assert.Equal(command.SKU, productFromDb.SKU);
        Assert.False(productFromDb.IsActive);
    }

    [Fact]
    public async Task Update_WithMissingProduct_ShouldFail()
    {
        var category = CategoryFactory.CreateValid(name: $"Cat-{Guid.NewGuid().ToString("N")[..8]}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new UpdateProductCommand
        {
            Id = Guid.NewGuid(),
            SKU = "SKU-MISS",
            BarCode = "BAR-MISS",
            ProductName = "Missing Product",
            Description = "Missing product update",
            SellingPrice = 20m,
            IsActive = true,
            Unit = Domain.Products.Enums.Unit.Piece,
            CategoryId = category.Id
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal("Product.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Delete_WithExistingProduct_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"D{unique}", categoryId: category.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new DeleteProductCommand(product.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.False(await _context.Products.AnyAsync(x => x.Id == product.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Delete_WithMissingProduct_ShouldFail()
    {
        var result = await _mediator.Send(new DeleteProductCommand(Guid.NewGuid()), CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal("Product.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Get_WithExistingProduct_ShouldReturnDto()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"G{unique}", categoryId: category.Id);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new GetProductQuery(product.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(product.Id, result.Value.Id);
        Assert.Equal(product.SKU, result.Value.SKU);
        Assert.Equal(category.Id, result.Value.CategoryId);
    }

    [Fact]
    public async Task Get_WithMissingProduct_ShouldFail()
    {
        var result = await _mediator.Send(new GetProductQuery(Guid.NewGuid()), CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal("Product.NotFound", result.TopError.Code);
    }

     
}
