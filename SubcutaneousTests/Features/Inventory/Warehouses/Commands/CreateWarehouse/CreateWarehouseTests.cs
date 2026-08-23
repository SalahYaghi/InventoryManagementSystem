using Contract.Common.Interfaces;
using Contract.Features.Inventory.Warehouses.Commands.CreateWarehouse;
using Contract.Features.Inventory.Warehouses.Commands.DeleteWarehouse;
using Contract.Features.Inventory.Warehouses.Commands.UpdateWarehouse;
using Contract.Features.Inventory.Warehouses.Queries.GetWarehouse;
using Contract.Features.Inventory.Warehouses.Queries.GetWarehousePaged;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Domain.Contacts.Address.Country;
using Domain.Warehouses;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Inventory.Warehouses.Commands.CreateWarehouse;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateWarehouseTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateWarehouseTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<(Guid CountryId, Guid CityId)> SeedLocationAsync()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Country.Create($"Palestine-{unique}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"Gaza-{unique}").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return (country.Id, city.Id);
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var location = await SeedLocationAsync();
        var command = new CreateWarehouseCommand
        {
            Name = $"Warehouse-{unique}",
            Code = $"WH-{unique}",
            Address = new CreateAddressCommand
            {
                CountryId = location.CountryId,
                CityId = location.CityId,
                PostalCode = "12345",
                BuildingNumber = "10",
                Street = "Main Street",
                Description = "Warehouse address"
            }
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.Equal(command.Code, result.Value.Code);
        Assert.True(await _context.Warehouses.AnyAsync(x => x.Id == result.Value.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithMissingAddress_ShouldFail()
    {
        var command = new CreateWarehouseCommand { Name = "Warehouse", Code = "WH-MISS", Address = null! };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithEmptyName_ShouldFail()
    {
        var location = await SeedLocationAsync();
        var command = new CreateWarehouseCommand
        {
            Name = string.Empty,
            Code = "WH-EMPTY",
            Address = new CreateAddressCommand { CountryId = location.CountryId, CityId = location.CityId, Street = "Main" }
        };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Update_WithExistingWarehouse_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var location = await SeedLocationAsync();
        var address = AddressFactory.CreateValid(countryId: location.CountryId, cityId: location.CityId);
        var warehouse = WarehouseFactory.CreateValid(name: $"Old-{unique}", code: $"OWH-{unique}", address: address);
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new UpdateWarehouseCommand
        {
            Id = warehouse.Id,
            Name = $"Updated-{unique}",
            Code = $"UWH-{unique}",
            WarehouseStatus = WarehouseStatus.Active,
            Address = new UpdateAddressCommand
            {
                CountryId = location.CountryId,
                CityId = location.CityId,
                PostalCode = "99999",
                BuildingNumber = "20",
                Street = "Updated Street",
                Description = "Updated address"
            }
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.Equal(command.Code, result.Value.Code);
    }

    [Fact]
    public async Task Update_WithMissingWarehouse_ShouldFail()
    {
        var command = new UpdateWarehouseCommand { Id = Guid.NewGuid(), Name = "Warehouse", Code = "WH-MISSING", WarehouseStatus = WarehouseStatus.Active };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("Warehouse.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Delete_WithMissingWarehouse_ShouldFail()
    {
        var result = await _mediator.Send(new DeleteWarehouseCommand(Guid.NewGuid()), CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("Warehouse.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task Get_WithMissingWarehouse_ShouldFail()
    {
        var result = await _mediator.Send(new GetWarehouseQuery(Guid.NewGuid()), CancellationToken.None);
        Assert.True(result.IsError);
        Assert.Equal("Warehouse.NotFound", result.TopError.Code);
    }

    [Fact]
    public async Task GetPaged_WithWarehouses_ShouldReturnList()
    {
        var location = await SeedLocationAsync();
        var warehouse = WarehouseFactory.CreateValid(name: $"List-{Guid.NewGuid().ToString("N")[..8]}", address: AddressFactory.CreateValid(countryId: location.CountryId, cityId: location.CityId));
        await _context.Warehouses.AddAsync(warehouse, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new GetWarehousesQuery(), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Contains(result.Value, x => x.Id == warehouse.Id);
    }
}
