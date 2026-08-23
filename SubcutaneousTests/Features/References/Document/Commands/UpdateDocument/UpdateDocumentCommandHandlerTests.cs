using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Microsoft.AspNetCore.Http;

namespace SubcutaneousTests.Features.References.Document.Commands.UpdateDocument;

[Collection(WebAppFactoryCollection.CollectionName)]
public class UpdateDocumentCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public UpdateDocumentCommandHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private static IFormFile CreateImageFile(string fileName = "updated-document.png")
    {
        var bytes = new byte[] { 137, 80, 78, 71, 1, 2, 3, 4 };
        var stream = new MemoryStream(bytes);
        return new FormFile(stream, 0, bytes.Length, "Image", fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };
    }

    private async Task<Domain.Document.Document> CreateSavedDocumentAsync()
    {
        var document = Domain.Document.Document.Create(Guid.NewGuid(), Domain.Document.DocumentType.Passport, "documents/original.png").Value;
        await _context.Documents.AddAsync(document, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return document;
    }

    [Fact]
    public async Task Handle_WithValidDataAndImage_ShouldSucceed()
    {
        var document = await CreateSavedDocumentAsync();
        var command = new global::Contract.Features.References.Documents.Commands.UpdateDocument.UpdateDocumentCommand { Id = document.Id, DocumentType = Domain.Document.DocumentType.NationalId, Image = CreateImageFile() };
        var result = await _mediator.Send(command, CancellationToken.None);
        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(Domain.Document.DocumentType.NationalId, result.Value.DocumentType);
    }

    [Fact]
    public async Task Handle_WithMissingDocument_ShouldFail()
    {
        var command = new global::Contract.Features.References.Documents.Commands.UpdateDocument.UpdateDocumentCommand { Id = Guid.NewGuid(), DocumentType = Domain.Document.DocumentType.NationalId, Image = CreateImageFile() };
        var result = await _mediator.Send(command, CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "Document.NotFound");
    }
}
