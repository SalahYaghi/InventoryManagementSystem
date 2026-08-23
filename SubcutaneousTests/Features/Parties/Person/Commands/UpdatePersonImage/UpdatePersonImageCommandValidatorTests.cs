using Contract.Features.Parties.Person.Commands.UpdatePersonImage;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Parties.Person.Commands.UpdatePersonImage;

public class UpdatePersonImageCommandValidatorTests
{
    private readonly UpdatePersonImageCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithEmptyPersonId_ShouldHaveValidationError()
    {
        var command = new UpdatePersonImageCommand { PersonId = Guid.Empty, Image = null };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.PersonId);
    }

    [Fact]
    public async Task Validate_WithValidPersonId_ShouldNotHavePersonIdValidationError()
    {
        var command = new UpdatePersonImageCommand { PersonId = Guid.NewGuid(), Image = null };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldNotHaveValidationErrorFor(x => x.PersonId);
    }
}
