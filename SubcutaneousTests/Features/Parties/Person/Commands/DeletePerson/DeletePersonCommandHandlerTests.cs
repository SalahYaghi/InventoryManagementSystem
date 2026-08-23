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

using Contract.Features.Parties.People.Commands.DeletePerson;

namespace SubcutaneousTests.Features.Parties.Person.Commands.DeletePerson;

[Collection(WebAppFactoryCollection.CollectionName)]
public class DeletePersonCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public DeletePersonCommandHandlerTests(WebAppFactory factory)
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

    [Fact]
    public async Task Handle_WithMissingPerson_ShouldFail()
    {
        var result = await _mediator.Send(new DeletePersonCommand(Guid.NewGuid()));
        Assert.True(result.IsError);
    }
}
