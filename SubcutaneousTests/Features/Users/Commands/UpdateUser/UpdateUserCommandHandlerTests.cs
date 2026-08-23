using Contract.Common.Interfaces;
using Contract.Features.User.Commands.CreateUser;
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

namespace SubcutaneousTests.Features.Users.Commands.UpdateUser;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateUserCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public UpdateUserCommandHandlerTests(WebAppFactory factory)
    {
        _mediator = factory.CreateMediator();
        _context = factory.CreateAppDbContext();
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var employee = await SeedEmployeeAsync();
        var user = await SeedUserAsync(employee.Id);

        var command = new UpdateUserCommand(
            user.Id,
            UniqueUsername("updated"),
            UniqueEmail("updated"),
            false,
            Role.Admin);



        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.username, result.Value.Username);
        Assert.Equal(command.email, result.Value.Email);
        Assert.Equal(command.isActive, result.Value.IsActive);
        Assert.Equal(command.role, result.Value.Role);

        _context.ClearChangeTracker();
        var userFromDb = await _context.Users.FirstAsync(x => x.Id == user.Id);
        Assert.Equal(command.username, userFromDb.Username);
        Assert.Equal(command.email, userFromDb.Email);
        Assert.False(userFromDb.IsActive);
        Assert.Equal(Role.Admin, userFromDb.Role);
    }

    [Fact]
    public async Task Handle_WithMissingUser_ShouldFail()
    {
        var command = new UpdateUserCommand(
            Guid.NewGuid(),
            UniqueUsername("missing"),
            UniqueEmail("missing"),
            true,
            Role.Admin);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithExistingUsernameForAnotherUser_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();
        var firstUser = await SeedUserAsync(employee.Id, username: UniqueUsername("first"));
        var secondUser = await SeedUserAsync(employee.Id, username: UniqueUsername("second"));

        var command = new UpdateUserCommand(
            secondUser.Id,
            firstUser.Username,
            UniqueEmail("newmail"),
            true,
            Role.Admin);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithExistingEmailForAnotherUser_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();
       // var employee2 = await SeedEmployeeAsync();
        var firstUser = await SeedUserAsync(employee.Id, username:"User10"  ,email: UniqueEmail("firstmail"));
        var secondUser = await SeedUserAsync(employee.Id, username: "User1011", email: UniqueEmail("secondmail"));

        var command = new UpdateUserCommand(
            secondUser.Id,
            UniqueUsername("newname"),
            firstUser.Email,
            true,
            Role.Admin);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithSameUsernameAndEmailForSameUser_ShouldSucceed()
    {
        var employee = await SeedEmployeeAsync();
        var user = await SeedUserAsync(employee.Id);

        var command = new UpdateUserCommand(
            user.Id,
            user.Username,
            user.Email,
            user.IsActive,
            user.Role);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WithInvalidUsername_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();
        var user = await SeedUserAsync(employee.Id);

        var command = new UpdateUserCommand(
            user.Id,
            "1bad_user",
            UniqueEmail("validmail"),
            true,
            Role.Admin);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithInvalidEmail_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();
        var user = await SeedUserAsync(employee.Id);

        var command = new UpdateUserCommand(
            user.Id,
            UniqueUsername("validname"),
            "not-email",
            true,
            Role.Admin);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithInvalidRole_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();
        var user = await SeedUserAsync(employee.Id);

        var command = new UpdateUserCommand(
            user.Id,
            UniqueUsername("role"),
            UniqueEmail("role"),
            true,
            (Role)999);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    private async Task<User> SeedUserAsync(Guid employeeId, string? username = null, string? email = null)
    {
        var user = UserFactory.CreateValid(
            username: username ?? UniqueUsername("user"),
            email: email ?? UniqueEmail("user"),
            employeeId: employeeId);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(CancellationToken.None);

        return user;
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
