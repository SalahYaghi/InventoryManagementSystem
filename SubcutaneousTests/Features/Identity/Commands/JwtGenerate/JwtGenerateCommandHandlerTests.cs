using Contract.Common.Interfaces;
using MediatR;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Identity.Commands.JwtGenerate;

[Collection(WebAppFactoryCollection.CollectionName)]
public class JwtGenerateCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public JwtGenerateCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithQuery_ShouldReturnResult()
    {
        var request = new global::Contract.Features.Identity.Commands.JwtGenerate.JwtGeneratCommand("test@example.com", "P@ssw0rd123!");

        var result = await _mediator.Send(request);

        _output.WriteLine(result?.ToString() ?? "No result returned");
        Assert.NotNull(result);
    }

}
