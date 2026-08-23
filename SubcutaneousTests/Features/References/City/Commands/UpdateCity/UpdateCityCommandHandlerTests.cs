using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Domain.Contacts.Address.Country;

namespace SubcutaneousTests.Features.References.City.Commands.UpdateCity;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateCityCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateCityCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<Domain.Contacts.Address.Country.City> CreateSavedCityAsync(string name)
    {
        var country = Domain.Contacts.Address.Country.Country.Create("Country For Empty City Id").Value;
        var city = Domain.Contacts.Address.Country.City.Create(Guid.NewGuid(), country.Id, name).Value;
        await _context.Countries.AddAsync(country, CancellationToken.None);
        await _context.Cities.AddAsync(city, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return city;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var city = await CreateSavedCityAsync("Old City");
        var command = new global::Contract.Features.References.Cities.Commands.UpdateCity.UpdateCityCommand { Id = city.Id, Name = "Updated City" };
        var result = await _mediator.Send(command, CancellationToken.None);
        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
    }

    [Fact]
    public async Task Handle_WithMissingCity_ShouldFail()
    {
        var command = new global::Contract.Features.References.Cities.Commands.UpdateCity.UpdateCityCommand { Id = Guid.NewGuid(), Name = "Missing" };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "City.NotFound");
    }

    [Fact]
    public async Task Handle_WithEmptyName_ShouldFail()
    {
        var city = await CreateSavedCityAsync("Will Fail");
        var command = new global::Contract.Features.References.Cities.Commands.UpdateCity.UpdateCityCommand { Id = city.Id, Name = string.Empty };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}
