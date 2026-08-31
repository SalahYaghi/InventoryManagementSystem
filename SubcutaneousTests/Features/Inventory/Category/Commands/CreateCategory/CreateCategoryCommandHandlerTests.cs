using Contract.Common.Interfaces;
using Contract.Features.Inventory.Categories.Commands.CreateCategory;
using Contract.Features.Inventory.Categories.Commands.DeleteCategory;
using Contract.Features.Inventory.Categories.Commands.UpdateCategory;
using Contract.Features.Inventory.Categories.Queries.GetCategory;
using Contract.Features.Inventory.Categories.Queries.GetCategoryPaged;
using InventoryManagement.Tests.Common.Factories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Inventory.Category.Commands.CreateCategory;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateCategoryCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateCategoryCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var command = new CreateCategoryCommand { Name = $"Cat-{unique}" };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.NotEqual(Guid.Empty, result.Value.Id);

        var categoryFromDb = await _context.Categories.FirstOrDefaultAsync(x => x.Id == result.Value.Id, CancellationToken.None);
        Assert.NotNull(categoryFromDb);
        Assert.Equal(command.Name, categoryFromDb!.Name);
    }

    [Fact]
    public async Task Handle_WithEmptyName_ShouldFail()
    {
        var command = new CreateCategoryCommand { Name = string.Empty };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithTooLongName_ShouldFail()
    {
        var command = new CreateCategoryCommand { Name = new string('A', 21) };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Update_WithExistingCategory_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category = CategoryFactory.CreateValid(name: $"Old-{unique}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new UpdateCategoryCommand { Id = category.Id, Name = $"New-{unique}" };
        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
        _context.ClearChangeTracker();
        var categoryFromDb = await _context.Categories.FirstAsync(x => x.Id == category.Id, CancellationToken.None);
        Assert.Equal(command.Name, categoryFromDb.Name);
    }

    [Fact]
    public async Task Update_WithMissingCategory_ShouldFail()
    {
        var command = new UpdateCategoryCommand { Id = Guid.NewGuid(), Name = "Updated" };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal("Category.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Update_WithEmptyName_ShouldFail()
    {
        var category = CategoryFactory.CreateValid(name: $"Cat-{Guid.NewGuid().ToString("N")[..8]}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new UpdateCategoryCommand { Id = category.Id, Name = string.Empty };
        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Delete_WithExistingCategory_ShouldSucceed()
    {
        var category = CategoryFactory.CreateValid(name: $"Delete-{Guid.NewGuid().ToString("N")[..8]}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new DeleteCategoryCommand(category.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.False(await _context.Categories.AnyAsync(x => x.Id == category.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Delete_WithMissingCategory_ShouldFail()
    {
        var result = await _mediator.Send(new DeleteCategoryCommand(Guid.NewGuid()), CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal("Category.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Get_WithExistingCategory_ShouldReturnDto()
    {
        var category = CategoryFactory.CreateValid(name: $"Get-{Guid.NewGuid().ToString("N")[..8]}");
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new GetCategoryQuery(category.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(category.Id, result.Value.Id);
        Assert.Equal(category.Name, result.Value.Name);
    }

    [Fact]
    public async Task Get_WithMissingCategory_ShouldFail()
    {
        var result = await _mediator.Send(new GetCategoryQuery(Guid.NewGuid()), CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Equal("Category.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task GetPaged_WithCategories_ShouldReturnList()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var category1 = CategoryFactory.CreateValid(name: $"P1-{unique}");
        var category2 = CategoryFactory.CreateValid(name: $"P2-{unique}");
        await _context.Categories.AddRangeAsync([category1, category2], CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new GetCategoryPagedQuery() , CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Contains(result.Value, x => x.Id == category1.Id);
        Assert.Contains(result.Value, x => x.Id == category2.Id);
    }
}
