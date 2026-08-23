using FluentValidation.TestHelper;
using Xunit;

using Microsoft.AspNetCore.Http;

namespace SubcutaneousTests.Features.References.Document.Commands.CreateDocument;

public class CreateDocumentCommandValidatorTests
{
    private readonly global::Contract.Features.References.Documents.Commands.CreateDocument.CreateDocumentCommandValidator _validator = new();

    private static IFormFile CreateImageFile()
    {
        var bytes = new byte[] { 1, 2, 3, 4 };
        var stream = new MemoryStream(bytes);
        return new FormFile(stream, 0, bytes.Length, "DocumentImage", "document.png")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };
    }

    [Fact]
    public async Task Validate_WithValidImage_ShouldNotHaveValidationError()
    {
        var command = new global::Contract.Features.References.Documents.Commands.CreateDocument.CreateDocumentCommand { DocumentType = Domain.Document.DocumentType.Passport, DocumentImage = CreateImageFile() };
        var result = await _validator.TestValidateAsync(command);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithNullImage_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.References.Documents.Commands.CreateDocument.CreateDocumentCommand { DocumentType = Domain.Document.DocumentType.Passport, DocumentImage = null };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.DocumentImage);
    }
}
