using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.Document.Commands.UpdateDocument;

public class UpdateDocumentCommandValidatorTests
{
    private readonly global::Contract.Features.References.Documents.Commands.UpdateDocument.UpdateDocumentCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidId_ShouldNotHaveValidationError()
    {
        var command = new global::Contract.Features.References.Documents.Commands.UpdateDocument.UpdateDocumentCommand { Id = Guid.NewGuid(), DocumentType = Domain.Document.DocumentType.NationalId, Image = null };
        var result = await _validator.TestValidateAsync(command);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyId_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.References.Documents.Commands.UpdateDocument.UpdateDocumentCommand { Id = Guid.Empty
            , DocumentType = Domain.Document.DocumentType.NationalId, Image = null };
       
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}
