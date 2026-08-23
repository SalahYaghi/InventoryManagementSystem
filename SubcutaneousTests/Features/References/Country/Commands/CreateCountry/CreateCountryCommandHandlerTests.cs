using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.Country.Commands.CreateCountry;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateCountryCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreateCountryCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var command = new global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommand
        {
            Id = Guid.NewGuid(),
            Name = $"Country-{unique}"
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Name, result.Value.Name);
        Assert.True(await _context.Countries.AnyAsync(x => x.Id == result.Value.Id && x.Name == command.Name, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithEmptyName_ShouldFail()
    {
        var command = new global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommand
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WithWhiteSpaceName_ShouldFail()
    {
        var command = new global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommand
        {
            Id = Guid.NewGuid(),
            Name = "   "
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_WithLongButValidName_ShouldSucceed()
    {
        var name = new string('A', 100);
        var command = new global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommand
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(name, result.Value.Name);
    }
}
