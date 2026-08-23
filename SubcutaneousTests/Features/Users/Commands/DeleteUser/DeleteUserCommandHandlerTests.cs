using Contract.Common.Interfaces;
using Contract.Features.Users.Commands.DeleteUser;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Identity.Employee;
using Domain.Identity.Users;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Identity;
using InventoryManagement.Tests.Common.Factories.People;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;

namespace SubcutaneousTests.Features.Users.Commands.DeleteUser;

[Collection(WebAppFactoryCollection.CollectionName)]
public class DeleteUserCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public DeleteUserCommandHandlerTests(WebAppFactory factory)
    {
        _mediator = factory.CreateMediator();
        _context = factory.CreateAppDbContext();
    }

    [Fact]
    public async Task Handle_WithExistingUser_ShouldSucceed()
    {
        var employee = await SeedEmployeeAsync();
        var user = UserFactory.CreateValid(
            username: UniqueUsername("delete"),
            email: UniqueEmail("delete"),
            employeeId: employee.Id);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new DeleteUserCommand(user.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.False(await _context.Users.AnyAsync(x => x.Id == user.Id));
    }

    [Fact]
    public async Task Handle_WithMissingUser_ShouldFail()
    {
        var command = new DeleteUserCommand(Guid.NewGuid());

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithMissingUser_ShouldNotDeleteOtherUsers()
    {
        var employee = await SeedEmployeeAsync();
        var user = UserFactory.CreateValid(
            username: UniqueUsername("keep"),
            email: UniqueEmail("keep"),
            employeeId: employee.Id);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new DeleteUserCommand(Guid.NewGuid());

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.True(await _context.Users.AnyAsync(x => x.Id == user.Id));
    }

    private async Task<Employee> SeedEmployeeAsync()
    {
        var suffix = UniqueSuffix();

        var country = Country.Create($"Country{suffix}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"City{suffix}");
        var address = AddressFactory.CreateValid(countryId: country.Id, cityId: city.Value.Id);
        var warehouseAddress = AddressFactory.CreateValid(countryId: country.Id, cityId: city.Value.Id);
        var contact = ContactInfoFactory.CreateValid(email: UniqueEmail("person"));
        var person = PersonFactory.CreateValid(nationalNo: UniqueNationalNo(), contact: contact, address: address);
        var warehouse = WarehouseFactory.CreateValid(
            id: Guid.NewGuid(),
            name: $"Warehouse{suffix}",
            code: $"WH{suffix}",
            address: warehouseAddress);
        var employee = EmployeeFactory.CreateValid(jobTitle: "Manager", person: person, warehouseId: warehouse.Id);

        await _context.Countries.AddAsync(country);
        await _context.Cities.AddAsync(city.Value);
        await _context.Addresses.AddAsync(address);
        await _context.Addresses.AddAsync(warehouseAddress);
        await _context.ContactInfos.AddAsync(contact);
        await _context.People.AddAsync(person);
        await _context.Warehouses.AddAsync(warehouse);
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync(CancellationToken.None);

        return employee;
    }

    private static string UniqueSuffix() => Guid.NewGuid().ToString("N")[..8];
    private static string UniqueUsername(string prefix) => $"{prefix}_{Guid.NewGuid().ToString("N")[..8]}";
    private static string UniqueEmail(string prefix) => $"{prefix}.{Guid.NewGuid().ToString("N")[..8]}@test.com";
    private static string UniqueNationalNo() => Random.Shared.Next(100000000, 999999999).ToString();
}
