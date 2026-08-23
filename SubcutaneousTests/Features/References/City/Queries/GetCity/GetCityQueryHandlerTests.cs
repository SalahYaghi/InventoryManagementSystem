using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Domain.Contacts.Address.Country;

namespace SubcutaneousTests.Features.References.City.Queries.GetCity;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetCityQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetCityQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithExistingCity_ShouldReturnDto()
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        var city = Domain.Contacts.Address.Country.City.Create(Guid.NewGuid(), country.Id, $"QueryCity-{Guid.NewGuid():N}").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new global::Contract.Features.References.Cities.Queries.GetCity.GetCityQuery(city.Id), CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(city.Id, result.Value.Id);
        Assert.Equal(city.Name, result.Value.Name);
    }

    [Fact]
    public async Task Handle_WithMissingCity_ShouldFail()
    {
        var result = await _mediator.Send(new global::Contract.Features.References.Cities.Queries.GetCity.GetCityQuery(Guid.NewGuid()), CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "City.NotFound");
    }
}
