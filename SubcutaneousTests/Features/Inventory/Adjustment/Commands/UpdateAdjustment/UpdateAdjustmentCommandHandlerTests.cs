using Contract.Common.Interfaces;
using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustment;
using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustmentDetailsQuantity;
using Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetail;
using Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetailPaged;
using Contract.Features.Inventory.Adjustments.Commands.CreateAdjustment;
using Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail;
using Contract.Features.Inventory.Adjustments.Commands.DeleteAdjustment;
using Contract.Features.Inventory.Adjustments.Queries.GetAdjustment;
using Contract.Features.Inventory.Adjustments.Queries.GetAdjustmentPaged;
using Contract.Features.Transactions.Order.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Order.Commands.DeleteOrderDetail;
using Contract.Features.Transactions.Order.Commands.UpdateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.UpdateOrder;
using Domain.Adjustments;
using Domain.Contacts.Address.Country;
using InventoryManagement.Tests.Common.Factories.Adjustments;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Inventory.Adjustment.Commands.UpdateAdjustment;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateAdjustmentCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateAdjustmentCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<(Guid WarehouseId, Guid ProductId, byte[] RowVersion)> SeedStockAsync(decimal quantity = 20m)
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        var address = AddressFactory.CreateValid(countryId: country.Id, cityId: city.Id);
        var warehouse = WarehouseFactory.CreateValid(name: $"Warehouse-{unique}", code: $"WH-{unique}", address: address);
        var category = CategoryFactory.CreateValid(name: $"Cat-{unique}");
        var product = ProductFactory.CreateValid(sku: $"S{unique}", categoryId: category.Id);
        var stock = WarehouseStockFactory.CreateValid(warehouseId: warehouse.Id, productId: product.Id, quantity: quantity);

        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.Categories.AddAsync(category, CancellationToken.None);
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.WarehouseStocks.AddAsync(stock, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var stockFromDb = await _context.WarehouseStocks.FirstAsync(x => x.Id == stock.Id, CancellationToken.None);
        return (warehouse.Id, product.Id, stockFromDb.RowVersion);
    }

    [Fact]
    public async Task Handle_WithValidIncreaseData_ShouldSucceed()
    {
        var setup = await SeedStockAsync();
        var command = new CreateAdjustmentCommand
        {
            WarehouseId = setup.WarehouseId,
            AdjustmentReason = AdjustmentReason.ExtraFound,
            Notes = "Increase stock after count",
            AdjustmentDetailCommands =
            [
                new CreateAdjustmentDetailInnerCommand
                {
                    ProductId = setup.ProductId,
                    Quantity = 5m,
                    RowVersion = setup.RowVersion
                }
            ]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(setup.WarehouseId, result.Value.WarehouseId);
        Assert.Single(result.Value.AdjustmentDetailDtos);
        Assert.True(await _context.Adjustments.AnyAsync(x => x.Id == result.Value.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithValidDecreaseData_ShouldSucceed()
    {
        var setup = await SeedStockAsync(quantity: 20m);
        var command = new CreateAdjustmentCommand
        {
            WarehouseId = setup.WarehouseId,
            AdjustmentReason = AdjustmentReason.Damaged,
            Notes = "Decrease damaged stock",
            AdjustmentDetailCommands =
            [
                new CreateAdjustmentDetailInnerCommand { ProductId = setup.ProductId, Quantity = 2m, RowVersion = setup.RowVersion }
            ]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(AdjustmentType.Decrease, result.Value.AdjustmentType);
    }

    [Fact]
    public async Task Handle_WithMissingWarehouse_ShouldFail()
    {
        var setup = await SeedStockAsync();
        var command = new CreateAdjustmentCommand
        {
            WarehouseId = Guid.NewGuid(),
            AdjustmentReason = AdjustmentReason.ExtraFound,
            AdjustmentDetailCommands =
            [
                new CreateAdjustmentDetailInnerCommand { ProductId = setup.ProductId, Quantity = 1m, RowVersion = setup.RowVersion }
            ]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithWrongRowVersion_ShouldFail()
    {
        var setup = await SeedStockAsync();
        var command = new CreateAdjustmentCommand
        {
            WarehouseId = setup.WarehouseId,
            AdjustmentReason = AdjustmentReason.ExtraFound,
            AdjustmentDetailCommands =
            [
                new CreateAdjustmentDetailInnerCommand { ProductId = setup.ProductId, Quantity = 1m, RowVersion = [9, 9, 9, 9] }
            ]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithDecreaseQuantityGreaterThanAvailable_ShouldFail()
    {
        var setup = await SeedStockAsync(quantity: 1m);
        var command = new CreateAdjustmentCommand
        {
            WarehouseId = setup.WarehouseId,
            AdjustmentReason = AdjustmentReason.Damaged,
            AdjustmentDetailCommands =
            [
                new CreateAdjustmentDetailInnerCommand { ProductId = setup.ProductId, Quantity = 300m, RowVersion = setup.RowVersion }
            ]
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Update_WithExistingAdjustment_ShouldSucceed()
    {
        var setup = await SeedStockAsync();
        var detail = AdjustmentDetailFactory.CreateValid(productId: setup.ProductId, quantity: 2m);
        var adjustment = AdjustmentFactory.CreateValid(warehouseId: setup.WarehouseId, adjustmentReason: AdjustmentReason.ExtraFound, adjustmentDetails: [detail], notes: "Old notes");
        await _context.AdjustmentDetails.AddAsync(detail, CancellationToken.None);
        await _context.Adjustments.AddAsync(adjustment, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new UpdateAdjustmentCommand { Id = adjustment.Id, Notes = "Updated notes" }, CancellationToken.None);

        Assert.True(result.IsSuccess);
        _context.ClearChangeTracker();

        var adjustmentFromDb = await _context.Adjustments.FirstAsync(x => x.Id == adjustment.Id, CancellationToken.None);
        Assert.Equal("Updated notes", adjustmentFromDb.Notes);
    }

    [Fact]
    public async Task Update_WithMissingAdjustment_ShouldFail()
    {
        var result = await _mediator.Send(new UpdateAdjustmentCommand { Id = Guid.NewGuid(), Notes = "Updated" }, CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("Adjustment.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Delete_WithDraftAdjustment_ShouldSucceed()
    {
        var setup = await SeedStockAsync();
        var detail = AdjustmentDetailFactory.CreateValid(productId: setup.ProductId, quantity: 2m);
        var adjustment = AdjustmentFactory.CreateValid(warehouseId: setup.WarehouseId, adjustmentReason: AdjustmentReason.ExtraFound, adjustmentDetails: [detail]);
        await _context.AdjustmentDetails.AddAsync(detail, CancellationToken.None);
        await _context.Adjustments.AddAsync(adjustment, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new DeleteAdjustmentCommand(adjustment.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.False(await _context.Adjustments.AnyAsync(x => x.Id == adjustment.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Delete_WithMissingAdjustment_ShouldFail()
    {
        var result = await _mediator.Send(new DeleteAdjustmentCommand(Guid.NewGuid()), CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("Adjustment.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task UpdateStatus_ToApproved_ShouldUpdateWarehouseStock()
    {
        var setup = await SeedStockAsync(quantity: 10m);
        var detail = AdjustmentDetailFactory.CreateValid(productId: setup.ProductId, quantity: 4m);
        var adjustment = AdjustmentFactory.CreateValid(warehouseId: setup.WarehouseId, adjustmentReason: AdjustmentReason.ExtraFound, adjustmentDetails: [detail]);
        await _context.AdjustmentDetails.AddAsync(detail, CancellationToken.None);
        await _context.Adjustments.AddAsync(adjustment, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new UpdateAdjustmentStatusCommand { Id = adjustment.Id, AdjustmentStatus = AdjustmentStatus.Approved }, CancellationToken.None);

        Assert.True(result.IsSuccess); _context.ClearChangeTracker();

        var stockFromDb = await _context.WarehouseStocks.FirstAsync(x => x.WarehouseId == setup.WarehouseId && x.ProductId == setup.ProductId, CancellationToken.None);
        Assert.Equal(14m, stockFromDb.Quantity);
    }

    [Fact]
    public async Task Get_WithExistingAdjustment_ShouldReturnDto()
    {
        var setup = await SeedStockAsync();
        var detail = AdjustmentDetailFactory.CreateValid(productId: setup.ProductId, quantity: 2m);
        var adjustment = AdjustmentFactory.CreateValid(warehouseId: setup.WarehouseId, adjustmentReason: AdjustmentReason.ExtraFound, adjustmentDetails: [detail]);
        await _context.AdjustmentDetails.AddAsync(detail, CancellationToken.None);
        await _context.Adjustments.AddAsync(adjustment, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new GetAdjustmentQuery(adjustment.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(adjustment.Id, result.Value.Id);
    }

    [Fact]
    public async Task Get_WithMissingAdjustment_ShouldFail()
    {
        var result = await _mediator.Send(new GetAdjustmentQuery(Guid.NewGuid()), CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("Adjustment.NotFound", result.TopError.Code);
    }
}
