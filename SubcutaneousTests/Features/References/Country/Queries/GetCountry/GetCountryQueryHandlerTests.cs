using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Domain.Contacts.Address.Country;

namespace SubcutaneousTests.Features.References.Country.Queries.GetCountry;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetCountryQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetCountryQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithExistingCountry_ShouldReturnDto()
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new global::Contract.Features.References.Countries.Queries.GetCountry.GetCountryQuery(country.Id), CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(country.Id, result.Value.Id);
        Assert.Equal(country.Name, result.Value.Name);
    }

    [Fact]
    public async Task Handle_WithMissingCountry_ShouldFail()
    {
        var result = await _mediator.Send(new global::Contract.Features.References.Countries.Queries.GetCountry.GetCountryQuery(Guid.NewGuid()), CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "Country.NotFound");
    }
}
