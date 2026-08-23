using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.Address.Queries.GetAddress;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetAddressQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetAddressQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
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

    [Fact]
    public async Task Handle_WithExistingAddress_ShouldReturnDto()
    {
        var address = await CreateSavedAddressAsync();
        var result = await _mediator.Send(new global::Contract.Features.References.Addresses.Queries.GetAddress.GetAddressQuery(address.Id), CancellationToken.None);
        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(address.Id, result.Value.Id);
        Assert.Equal(address.CountryId, result.Value.CountryId);
        Assert.Equal(address.CityId, result.Value.CityId);
    }

    [Fact]
    public async Task Handle_WithMissingAddress_ShouldFail()
    {
        var result = await _mediator.Send(new global::Contract.Features.References.Addresses.Queries.GetAddress.GetAddressQuery(Guid.NewGuid()), CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "Address.NotFound");
    }
}
