using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Domain.Contacts.Address.Country;

namespace SubcutaneousTests.Features.References.City.Commands.CreateCity;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateCityCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateCityCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        var unique = Guid.NewGuid().ToString("N")[..8];
        var command = new global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommand { Id = Guid.NewGuid(), CountryId = country.Id, Name = $"City-{unique}" };

        var result = await _mediator.Send(command, CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.True(await _context.Cities.AnyAsync(x => x.Id == command.Id && x.CountryId == country.Id, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyId_ShouldFail()
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        var command = new global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommand { Id = Guid.Empty, CountryId = country.Id, Name = "Gaza" };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WithEmptyName_ShouldFail()
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        var command = new global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommand { Id = Guid.NewGuid(), CountryId = country.Id, Name = string.Empty };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}
