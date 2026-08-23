using Contract.Common.Interfaces;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Customers;
using InventoryManagement.Tests.Common.Factories.Identity;
using InventoryManagement.Tests.Common.Factories.People;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Suppliers;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Contract.Features.Parties.Employees.Commands.CreateEmployeeWithId;
using Contract.Features.Parties.Employees.Commands.CreateEmployeeWithPerson;
using Contract.Features.Parties.Employees.Commands.UpdateEmployee;
using Contract.Features.Parties.Employees.Commands.DeleteEmployee;
using Contract.Features.Parties.People.Commands.CreatePerson;


namespace SubcutaneousTests.Features.Parties.Employees.Commands.CreateEmployeeWithId;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateEmployeeCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public CreateEmployeeCommandHandlerTests(WebAppFactory factory)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
    }
    private static async Task<(Domain.Contacts.Address.Country.Country Country, Domain.Contacts.Address.Country.City City)> SeedCountryAndCityAsync(IAppDbContext context)
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Domain.Contacts.Address.Country.Country.Create($"Country-{unique}").Value;
        var city = Domain.Contacts.Address.Country.City.Create(Guid.NewGuid(), country.Id, $"City-{unique}").Value;
        await context.Countries.AddAsync(country);
        await context.Cities.AddAsync(city);
        await context.SaveChangesAsync(CancellationToken.None);
        return (country, city);
    }
     

    private static CreateContactInfoCommand CreateContactCommand(string? email = null)
    {
        var unique = Guid.NewGuid().ToString("N")[..10];
        return new CreateContactInfoCommand
        {
            Email = email ?? $"contact-{unique}@test.com",
            PhoneNumber = "+970599999999",
            AlternitavePhoneNumber = "+970598888888",
            FaxNumber = "+9702222222",
            WebsiteUrl = "https://example.com"
        };
    }

    private static CreateAddressCommand CreateAddressCommand(Guid countryId, Guid cityId)
    {
        return new CreateAddressCommand
        {
            CountryId = countryId,
            CityId = cityId,
            PostalCode = "12345",
            BuildingNumber = "10",
            Street = "Main Street",
            Description = "Valid address"
        };
    }

    private static UpdateContactInfoCommand CreateUpdateContactCommand(Guid? id = null)
    {
        var unique = Guid.NewGuid().ToString("N")[..10];
        return new UpdateContactInfoCommand
        {
            Id = id,
            Email = $"updated-{unique}@test.com",
            PhoneNumber = "+970599111111",
            AlternitavePhoneNumber = "+970598111111",
            FaxNumber = "+9702222222",
            WebsiteUrl = "https://updated.example.com"
        };
    }

    private static UpdateAddressCommand CreateUpdateAddressCommand(Guid countryId, Guid cityId, Guid? id = null)
    {
        return new UpdateAddressCommand
        {
            Id = id,
            CountryId = countryId,
            CityId = cityId,
            PostalCode = "54321",
            BuildingNumber = "20",
            Street = "Updated Street",
            Description = "Updated address"
        };
    }

}
