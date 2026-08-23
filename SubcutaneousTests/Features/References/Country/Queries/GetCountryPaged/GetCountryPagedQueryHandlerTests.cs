using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Domain.Contacts.Address.Country;

namespace SubcutaneousTests.Features.References.Country.Queries.GetCountryPaged;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetCountryPagedQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetCountryPagedQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithExistingCountries_ShouldReturnList()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var first = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        var second = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        await _context.Countries.AddRangeAsync(first, second);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new global::Contract.Features.References.Countries.Queries.GetCountryPaged.GetCountryPagedQuery(), CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Contains(result.Value, x => x.Id == first.Id);
        Assert.Contains(result.Value, x => x.Id == second.Id);
    }
}
