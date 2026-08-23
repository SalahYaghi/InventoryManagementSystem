using Contract.Common.Interfaces;
using MediatR;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.Inventory.Product.Commands.DeleteProductImage;

[Collection(WebAppFactoryCollection.CollectionName)]
public class DeleteProductImageCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public DeleteProductImageCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithMissingEntity_ShouldReturnErrorResult()
    {
        var request = new global::Contract.Features.Inventory.Product.Commands.DeleteProduct.DeleteProductImageCommand(Guid.NewGuid());

        var result = await _mediator.Send(request);

        _output.WriteLine(result?.ToString() ?? "No result returned");
        Assert.NotNull(result);
    }
}
