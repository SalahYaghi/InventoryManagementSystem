using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.Address.Queries.GetAddressPaged;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetAddressPagedQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetAddressPagedQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<(Domain.Contacts.Address.Country.Country Country, Domain.Contacts.Address.Country.City City)> CreateSavedCountryAndCityAsync()
    {
        var country = Domain.Contacts.Address.Country.Country.Create($"AddressCountry-{Guid.NewGuid():N}").Value;
        var city = Domain.Contacts.Address.Country.City.Create(Guid.NewGuid(), country.Id, $"AddressCity-{Guid.NewGuid():N}").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return (country, city);
    }

    private async Task<Domain.Contacts.Address.Address> CreateSavedAddressAsync()
    {
        var (country, city) = await CreateSavedCountryAndCityAsync();
        var address = Domain.Contacts.Address.Address.Create(
            Guid.NewGuid(),
            country.Id,
            city.Id,
            "12345",
            "10",
            "Main Street",
            "Valid description").Value;
        await _context.Addresses.AddAsync(address, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return address;
    }
}
