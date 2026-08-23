using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Domain.Contacts.Address.Country;

namespace SubcutaneousTests.Features.References.Country.Commands.UpdateCountry;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateCountryCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateCountryCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var command = new global::Contract.Features.References.Countries.Commands.UpdateCountry.UpdateCountryCommand { Id = country.Id, Name = $"UpdatedCountry-{unique}" };
        var result = await _mediator.Send(command, CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
        _context.ClearChangeTracker();
        var fromDb = await _context.Countries.FirstAsync(x => x.Id == country.Id, CancellationToken.None);
        Assert.Equal(command.Name, fromDb.Name);
    }

    [Fact]
    public async Task Handle_WithMissingCountry_ShouldFail()
    {
        var command = new global::Contract.Features.References.Countries.Commands.UpdateCountry.UpdateCountryCommand { Id = Guid.NewGuid(), Name = "Missing Country" };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "Country.NotFound");
    }

    [Fact]
    public async Task Handle_WithEmptyName_ShouldFail()
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        var command = new global::Contract.Features.References.Countries.Commands.UpdateCountry.UpdateCountryCommand { Id = country.Id, Name = string.Empty };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}
