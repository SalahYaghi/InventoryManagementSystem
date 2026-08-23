using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Microsoft.AspNetCore.Http;

namespace SubcutaneousTests.Features.References.Document.Commands.CreateDocument;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreatePersonDocumentCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public CreatePersonDocumentCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private static IFormFile CreateImageFile(string fileName = "document.png")
    {
        var bytes = new byte[] { 137, 80, 78, 71, 1, 2, 3, 4 };
        var stream = new MemoryStream(bytes);
        return new FormFile(stream, 0, bytes.Length, "DocumentImage", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };
    }

 

    [Fact]
    public async Task Handle_WithNullImage_ShouldFail()
    {
        var command = new global::Contract.Features.References.Documents.Commands.CreateDocument.CreateDocumentCommand
        {
            DocumentType = Domain.Document.DocumentType.Passport,
            DocumentImage = null
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
