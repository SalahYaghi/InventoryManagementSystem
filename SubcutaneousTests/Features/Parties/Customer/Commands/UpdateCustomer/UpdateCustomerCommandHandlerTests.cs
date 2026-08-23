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

using Contract.Features.Parties.Customers.Commands.UpdateCustomer;

namespace SubcutaneousTests.Features.Parties.Customer.Commands.UpdateCustomer;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateCustomerCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateCustomerCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithMissingCustomer_ShouldFail()
    {
        var graph = await SeedCountryAndCityAsync(_context);


        var command = new UpdateCustomerCommand
        {
            Id = Guid.NewGuid(),
            CustomerName = "Missing Customer",
            CustomerCode = "CUS-MISSING",
            Contact = CreateUpdateContactCommand(),
            Address = CreateUpdateAddressCommand(graph.Country.Id, graph.City.Id),
            Notes = "missing customer"
        };

        var result = await _mediator.Send(command);

        Assert.True(result.IsError);
    }

    

    [Fact]
    public async Task Handle_WithInvalidContact_ShouldFail()
    {
        var graph = await SeedCountryAndCityAsync(_context);
        var customer = CustomerFactory.CreateValid(customerCode: $"CU_0390" , 
            address:AddressFactory.CreateValid(countryId:graph.Country.Id , cityId:graph.City.Id));
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new UpdateCustomerCommand
        {
            Id = customer.Id,
            CustomerName = "Updated Customer",
            CustomerCode = "CUS-UPD",
            Contact = new UpdateContactInfoCommand { Id = customer.Contact?.Id, Email = "bad", PhoneNumber = "+970599111111", AlternitavePhoneNumber = "+970598111111" },
            Address = CreateUpdateAddressCommand(graph.Country.Id, graph.City.Id, customer.Address?.Id),
            Notes = "updated customer"
        };

        var result = await _mediator.Send(command);

        Assert.True(result.IsError);
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
