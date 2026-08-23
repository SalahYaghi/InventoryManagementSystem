using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace SubcutaneousTests.Features.References.Document.Queries.GetDocumentImage;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetPersonImageQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;
    private readonly ITestOutputHelper _output;

    public GetPersonImageQueryHandlerTests(WebAppFactory factory, ITestOutputHelper output)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
        _output = output;
    }

    [Fact]
    public async Task Handle_WithDocumentImage_ShouldReturnFileDto()
    {
        var document = Domain.Document.Document.Create(Guid.NewGuid(), Domain.Document.DocumentType.Passport, "documents/image.png").Value;
        await _context.Documents.AddAsync(document, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);

        var result = await _mediator.Send(new global::Contract.Features.Parties.Person.Queries.GetPersonImage.GeDocumentImageQuery(document.Id), CancellationToken.None);

        _output.WriteLine($"Result: {string.Join(", ", result.Errors.Select(e => e.Code + " " + e.Description))}");
        Assert.True(result.IsSuccess);
        Assert.Equal(document.ImageUrl, result.Value.FileUrl);
    }

    [Fact]
    public async Task Handle_WithMissingDocument_ShouldFail()
    {
        var result = await _mediator.Send(new global::Contract.Features.Parties.Person.Queries.GetPersonImage.GeDocumentImageQuery(Guid.NewGuid()), CancellationToken.None);
        Assert.False(result.IsSuccess);
    }
}
