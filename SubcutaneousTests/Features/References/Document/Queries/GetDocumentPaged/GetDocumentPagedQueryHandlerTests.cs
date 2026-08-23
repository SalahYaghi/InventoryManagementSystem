using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.Document.Queries.GetDocumentPaged;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetDocumentPagedQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetDocumentPagedQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    private async Task<Domain.Document.Document> CreateSavedDocumentAsync(string imageUrl)
    {
        var document = Domain.Document.Document.Create(Guid.NewGuid(), Domain.Document.DocumentType.Passport, imageUrl).Value;
        await _context.Documents.AddAsync(document, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        return document;
    }

    [Fact]
    public async Task Handle_WithExistingDocuments_ShouldReturnPaginatedResult()
    {
        var first = await CreateSavedDocumentAsync("documents/paged-one.png");
        var second = await CreateSavedDocumentAsync("documents/paged-two.png");
        var query = new global::Contract.Features.References.Documents.Queries.GetDocumentPaged.GetDocumentPagedQuery { PageNumber = 1, PageSize = 10 };
        var result = await _mediator.Send(query, CancellationToken.None);
        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Contains(result.Value.Items, x => x.Id == first.Id);
        Assert.Contains(result.Value.Items, x => x.Id == second.Id);
    }
}
