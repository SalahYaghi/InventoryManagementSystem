using Contract.Common.Interfaces;
using Contract.Features.User.Commands.CreateUser;
using Domain.Contacts.Address;
using Domain.Contacts.Address.Country;
using Domain.Identity.Employee;
using Domain.Identity.Users;
using Domain.People;
using Domain.Warehouses;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Identity;
using InventoryManagement.Tests.Common.Factories.People;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Users.Commands.CreateUser;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateUserCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateUserCommandHandlerTests(WebAppFactory factory , ITestOutputHelper testOutput)
    {
        _mediator = factory.CreateMediator();
        _context = factory.CreateAppDbContext();
        _output = testOutput;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var employee = await SeedEmployeeAsync();

        var command = new CreateUserCommand(
            UniqueUsername("user"),
            "P@ssw0rd123!",
            Role.Admin,
            UniqueEmail("created"),
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(command.username, result.Value.Username);
        Assert.Equal(command.email, result.Value.Email);
        Assert.Equal(command.employeeId, result.Value.EmployeeId);

        var userFromDb = await _context.Users.FirstOrDefaultAsync(x => x.Username == command.username);
        Assert.NotNull(userFromDb);
        Assert.Equal(command.email, userFromDb!.Email);
        Assert.Equal(command.employeeId, userFromDb.EmployeeId);
    }

    [Fact]
    public async Task Handle_WithExistingUsername_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();
        var existingUser = UserFactory.CreateValid(
            username: UniqueUsername("same"),
            email: UniqueEmail("old"),
            employeeId: employee.Id);

        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new CreateUserCommand(
            existingUser.Username,
            "P@ssw0rd123!",
            Role.Admin,
            UniqueEmail("new"),
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithExistingEmail_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();
        var existingUser = UserFactory.CreateValid(
            username: UniqueUsername("old"),
            email: UniqueEmail("same"),
            employeeId: employee.Id);

        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new CreateUserCommand(
            UniqueUsername("new"),
            "P@ssw0rd123!",
            Role.Admin,
            existingUser.Email,
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithMissingEmployee_ShouldFail()
    {
        var command = new CreateUserCommand(
            UniqueUsername("missing"),
            "P@ssw0rd123!",
            Role.Admin,
            UniqueEmail("missing"),
            Guid.NewGuid());

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithInvalidPassword_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();

        var command = new CreateUserCommand(
            UniqueUsername("weak"),
            "password",
            Role.Admin,
            UniqueEmail("weak"),
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithInvalidUsername_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();

        var command = new CreateUserCommand(
            "1bad_user",
            "P@ssw0rd123!",
            Role.Admin,
            UniqueEmail("baduser"),
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithInvalidEmail_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();

        var command = new CreateUserCommand(
            UniqueUsername("badmail"),
            "P@ssw0rd123!",
            Role.Admin,
            "not-email",
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WithInvalidRole_ShouldFail()
    {
        var employee = await SeedEmployeeAsync();

        var command = new CreateUserCommand(
            UniqueUsername("role"),
            "P@ssw0rd123!",
            (Role)999,
            UniqueEmail("role"),
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);
     
        _output.WriteLine($"Received errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_WhenCommandFails_ShouldNotAddUser()
    {
        var command = new CreateUserCommand(
            UniqueUsername("notadded"),
            "P@ssw0rd123!",
            Role.Admin,
            UniqueEmail("notadded"),
            Guid.NewGuid());

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.False(await _context.Users.AnyAsync(x => x.Username == command.username));
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldStoreHashedPasswordNotPlainPassword()
    {
        var employee = await SeedEmployeeAsync();

        var command = new CreateUserCommand(
            UniqueUsername("hash"),
            "P@ssw0rd123!",
            Role.Admin,
            UniqueEmail("hash"),
            employee.Id);

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);

        var userFromDb = await _context.Users.FirstAsync(x => x.Username == command.username);
        Assert.NotEqual(command.password, userFromDb.HashedPassword);
        Assert.False(string.IsNullOrWhiteSpace(userFromDb.HashedPassword));
    }

    private async Task<Employee> SeedEmployeeAsync()
    {
        var suffix = UniqueSuffix();

        var country = Country.Create($"Country{suffix}").Value;
        var city = City.Create(Guid.NewGuid(), country.Id, $"City{suffix}");
        var address = AddressFactory.CreateValid(
            countryId: country.Id,
            cityId: city.Value.Id,
            postalCode: "12345",
            buildingNumber: "10",
            street: "Street",
            description: "Address");

        var warehouseAddress = AddressFactory.CreateValid(
            countryId: country.Id,
            cityId: city.Value.Id,
            postalCode: "54321",
            buildingNumber: "20",
            street: "Warehouse",
            description: "Warehouse address");

        var contact = ContactInfoFactory.CreateValid(email: UniqueEmail("person"));
        var person = PersonFactory.CreateValid(
            nationalNo: UniqueNationalNo(),
            contact: contact,
            address: address);

        var warehouse = WarehouseFactory.CreateValid(
            id: Guid.NewGuid(),
            name: $"Warehouse{suffix}",
            code: $"WH{suffix}",
            address: warehouseAddress);

        var employee = EmployeeFactory.CreateValid(
            jobTitle: "Manager",
            person: person,
            warehouseId: warehouse.Id);

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

    private static string UniqueSuffix()
    {
        return Guid.NewGuid().ToString("N")[..8];
    }

    private static string UniqueUsername(string prefix)
    {
        return $"{prefix}_{Guid.NewGuid().ToString("N")[..8]}";
    }

    private static string UniqueEmail(string prefix)
    {
        return $"{prefix}.{Guid.NewGuid().ToString("N")[..8]}@test.com";
    }

    private static string UniqueNationalNo()
    {
        return Random.Shared.Next(100000000, 999999999).ToString();
    }
}
