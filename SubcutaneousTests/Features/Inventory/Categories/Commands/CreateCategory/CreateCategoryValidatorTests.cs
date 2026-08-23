using Contract.Features.Inventory.Categories.Commands.CreateCategory;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Categories.Commands.CreateCategory;

public class CreateCategoryValidatorTests
{
    private readonly CreateCategoryCommandValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new CreateCategoryCommand { Name = "Spare Parts" };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldHaveValidationError()
    {
        var command = new CreateCategoryCommand { Name = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_WithWhitespaceName_ShouldHaveValidationError()
    {
        var command = new CreateCategoryCommand { Name = "   " };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_WithTooLongName_ShouldHaveValidationError()
    {
        var command = new CreateCategoryCommand { Name = new string('A', 21) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_WithMaxLengthName_ShouldNotHaveValidationError()
    {
        var command = new CreateCategoryCommand { Name = new string('A', 20) };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}
