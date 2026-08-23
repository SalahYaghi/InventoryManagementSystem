using Contract.Features.Inventory.Product.Commands.UpdateProduct;
using Domain.Products.Enums;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Product.Commands.UpdateProduct;

public class UpdateProductCommandValidatorTests
{
    private readonly Contract.Features.Inventory.Product.Commands.UpdateProduct.UpdateProductCommandValidator _validator = new();

    private static Contract.Features.Inventory.Product.Commands.UpdateProduct.UpdateProductCommand ValidCommand() => new()
    {
        SKU = "SKU-OK",
        BarCode = "BAR-OK",
        ProductName = "Engine Oil",
        Description = "Valid product",
        SellingPrice = 10m,
        IsActive = true,
        Unit = Domain.Products.Enums.Unit.Piece,
        Id = Guid.NewGuid(),
        CategoryId = Guid.NewGuid()
    };

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() => _validator.TestValidate(ValidCommand()).ShouldNotHaveAnyValidationErrors();
    [Fact] public void Validate_WithEmptySku_ShouldHaveValidationError() { var c = ValidCommand() with { SKU = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.SKU); }
    [Fact] public void Validate_WithTooLongSku_ShouldHaveValidationError() { var c = ValidCommand() with { SKU = new string('S', 11) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.SKU); }
    [Fact] public void Validate_WithTooLongBarCode_ShouldHaveValidationError() { var c = ValidCommand() with { BarCode = new string('B', 51) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.BarCode); }
    [Fact] public void Validate_WithEmptyProductName_ShouldHaveValidationError() { var c = ValidCommand() with { ProductName = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.ProductName); }
    [Fact] public void Validate_WithTooLongProductName_ShouldHaveValidationError() { var c = ValidCommand() with { ProductName = new string('P', 31) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.ProductName); }
    [Fact] public void Validate_WithTooLongDescription_ShouldHaveValidationError() { var c = ValidCommand() with { Description = new string('D', 501) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Description); }
    [Fact] public void Validate_WithNegativeSellingPrice_ShouldHaveValidationError() { var c = ValidCommand() with { SellingPrice = -1m }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.SellingPrice); }
    [Fact] public void Validate_WithEmptyCategoryId_ShouldHaveValidationError() { var c = ValidCommand() with { CategoryId = Guid.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.CategoryId); }
    [Fact] public void Validate_WithZeroSellingPrice_ShouldNotHaveValidationError() { var c = ValidCommand() with { SellingPrice = 0m }; _validator.TestValidate(c).ShouldNotHaveValidationErrorFor(x => x.SellingPrice); }
}
