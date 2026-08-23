using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.Address.Commands.UpdateAddress;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateAddressCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateAddressCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
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
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var address = await CreateSavedAddressAsync();
        var (newCountry, newCity) = await CreateSavedCountryAndCityAsync();
        var command = new global::Contract.Features.References.Addresses.Commands.UpdateAddress.UpdateAddressCommand
        {
            Id = address.Id,
            CountryId = newCountry.Id,
            CityId = newCity.Id,
            PostalCode = "54321",
            BuildingNumber = "20",
            Street = "Updated Street",
            Description = "Updated description"
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(command.CountryId, result.Value.CountryId);
        Assert.Equal(command.CityId, result.Value.CityId);
        Assert.Equal(command.Street, result.Value.Street);
    }

    [Fact]
    public async Task Handle_WithMissingAddress_ShouldFail()
    {
        var (country, city) = await CreateSavedCountryAndCityAsync();
        var command = new global::Contract.Features.References.Addresses.Commands.UpdateAddress.UpdateAddressCommand { Id = Guid.NewGuid(), CountryId = country.Id, CityId = city.Id, PostalCode = "12345", BuildingNumber = "10", Street = "Main", Description = "Valid" };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "Address.NotFound");
    }

    [Fact]
    public async Task Handle_WithInvalidEmptyCityId_ShouldFail()
    {
        var address = await CreateSavedAddressAsync();
        var command = new global::Contract.Features.References.Addresses.Commands.UpdateAddress.UpdateAddressCommand { Id = address.Id, CountryId = address.CountryId, CityId = Guid.Empty, PostalCode = "12345", BuildingNumber = "10", Street = "Main", Description = "Valid" };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}
