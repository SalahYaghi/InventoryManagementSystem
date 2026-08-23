using Contract.Features.References.Documents.Commands.CreateDocument;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Parties.Person.Commands.CreatePersonDocument;

public class CreatePersonDocumentCommandValidatorTests
{
    private readonly CreatePersonDocumentCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithEmptyPersonId_ShouldHaveValidationError()
    {
        var command = new CreatePersonDocumentCommand { PersonId = Guid.Empty, Document = new CreateDocumentCommand { DocumentImage = null } };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.PersonId);
    }

    [Fact]
    public async Task Validate_WithMissingDocumentImage_ShouldHaveValidationError()
    {
        var command = new CreatePersonDocumentCommand { PersonId = Guid.NewGuid(), Document = new CreateDocumentCommand { DocumentImage = null } };
        var result = await _validator.TestValidateAsync(command);
        Assert.False(result.IsValid);
    }
}
