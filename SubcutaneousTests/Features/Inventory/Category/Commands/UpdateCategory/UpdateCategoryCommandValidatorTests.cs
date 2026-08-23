using Contract.Features.Inventory.Categories.Commands.UpdateCategory;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandValidatorTests
{
    private readonly UpdateCategoryCommandValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new UpdateCategoryCommand { Id = Guid.NewGuid(), Name = "Spare Parts" };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyId_ShouldHaveValidationError()
    {
        var command = new UpdateCategoryCommand { Id = Guid.Empty, Name = "Spare Parts" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldHaveValidationError()
    {
        var command = new UpdateCategoryCommand { Id = Guid.NewGuid(), Name = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_WithTooLongName_ShouldHaveValidationError()
    {
        var command = new UpdateCategoryCommand { Id = Guid.NewGuid(), Name = new string('A', 21) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
