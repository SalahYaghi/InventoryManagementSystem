using Contract.Common.Interfaces;
using Contract.Features.Parties.Employees.Commands.CreateEmployeeWithId;
using Contract.Features.Parties.Employees.Commands.CreateEmployeeWithPerson;
using Contract.Features.Parties.Employees.Commands.DeleteEmployee;
using Contract.Features.Parties.Employees.Commands.UpdateEmployee;
using Contract.Features.Parties.People.Commands.CreatePerson;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Warehouses;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Customers;
using InventoryManagement.Tests.Common.Factories.Identity;
using InventoryManagement.Tests.Common.Factories.People;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Suppliers;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SubcutaneousTests.Common;
using SubcutaneousTests.Features.Inventory.WarehouseStock.Commands.CreateWarehouseStock;
using Xunit;
using Xunit.Abstractions;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;


namespace SubcutaneousTests.Features.Parties.Employees.Commands.UpdateEmployee;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateEmployeeCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public UpdateEmployeeCommandHandlerTests(WebAppFactory factory)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
    }

    [Fact]
    public async Task Handle_WithMissingEmployee_ShouldFail()
    {

        var country = Country.Create("Country");
        var city = City.Create(Guid.NewGuid() , country.Value.Id , "city");
        _context.Countries.Add(country.Value);
        _context.Cities.Add(city.Value);
        var address = Address.Create(Guid.NewGuid() , country.Value.Id , city.Value.Id , "Salah" , "Mohammed" , "Yaghi" , "Saleh"); 
        _context.Addresses.Add(address.Value);

        var warehouse = Warehouse.Create(  Guid.NewGuid(), "name", "code", address.Value);
        _context.Warehouses.Add(warehouse.Value);

        await _context.SaveChangesAsync(CancellationToken.None);


        var result = await _mediator.Send(new UpdateEmployeeCommand(Guid.NewGuid(), "Supervisor", new DateOnly(2024, 1, 1), warehouse.Value.Id));
        Assert.True(result.IsError);
    }
}
