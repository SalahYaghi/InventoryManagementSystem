using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.Document.Queries.GetDocument;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetDocumentQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetDocumentQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<Domain.Document.Document> CreateSavedDocumentAsync()
    {
        var document = Domain.Document.Document.Create(Guid.NewGuid(), Domain.Document.DocumentType.Passport, "documents/query.png").Value;
        await _context.Documents.AddAsync(document, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return document;
    }

    [Fact]
    public async Task Handle_WithExistingDocument_ShouldReturnDto()
    {
        var document = await CreateSavedDocumentAsync();
        var result = await _mediator.Send(new global::Contract.Features.References.Documents.Queries.GetDocument.GetDocumentQuery(document.Id), CancellationToken.None);
        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(document.Id, result.Value.Id);
        Assert.Equal(document.DocumentType, result.Value.DocumentType);
    }

    [Fact]
    public async Task Handle_WithMissingDocument_ShouldFail()
    {
        var result = await _mediator.Send(new global::Contract.Features.References.Documents.Queries.GetDocument.GetDocumentQuery(Guid.NewGuid()), CancellationToken.None);
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "Document.NotFound");
    }
}
