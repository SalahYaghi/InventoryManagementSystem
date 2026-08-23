using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Product.Commands.CreateProductImage;

public class CreateProductImageValidatorTests
{
    private readonly global::Contract.Features.Inventory.Product.Commands.CreateProductImage.CreateProductImageValidator _validator = new();

    [Fact]
    public async Task Validate_WithEmptyOrDefaultData_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.Inventory.Product.Commands.CreateProductImage.CreateProductImageCommand(Guid.Empty, null);

        var result = await _validator.TestValidateAsync(command);

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithValidData_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.Inventory.Product.Commands.CreateProductImage.CreateProductImageCommand(Guid.NewGuid(), null);

        var result = await _validator.TestValidateAsync(command);

        Assert.False(result.IsValid);
    }
}
