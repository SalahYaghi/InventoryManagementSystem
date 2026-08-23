using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Domain.Contacts.Address.Country;

namespace SubcutaneousTests.Features.References.City.Queries.GetCityPaged;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetCityPagedQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetCityPagedQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithExistingCitiesForCountry_ShouldReturnOnlyThatCountryCities()
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        var otherCountry = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        var city1 = Domain.Contacts.Address.Country.City.Create(Guid.NewGuid(), country.Id, $"PagedCityA-{Guid.NewGuid():N}").Value;
        var city2 = Domain.Contacts.Address.Country.City.Create(Guid.NewGuid(), country.Id, $"PagedCityB-{Guid.NewGuid():N}").Value;
        var otherCity = Domain.Contacts.Address.Country.City.Create(Guid.NewGuid(), otherCountry.Id, $"OtherPagedCity-{Guid.NewGuid():N}").Value;
        await _context.Countries.AddRangeAsync(country, otherCountry);
        await _context.Cities.AddRangeAsync(city1, city2, otherCity);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new global::Contract.Features.References.Cities.Queries.GetCityPaged.GetCityByCountryIdPagedQuery(country.Id), CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Contains(result.Value, x => x.Id == city1.Id);
        Assert.Contains(result.Value, x => x.Id == city2.Id);
        Assert.DoesNotContain(result.Value, x => x.Id == otherCity.Id);
    }
}
