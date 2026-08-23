using Contract.Features.Parties.SupplierProducts.Commands.UpdateSupplierProduct;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Parties.SupplierProduct.Commands.UpdateSupplierProduct;

public class UpdateSupplierProductCommandValidatorTests
{
    private readonly UpdateSupplierProductCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptySupplierId_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { SupplierId = Guid.Empty });
        result.ShouldHaveValidationErrorFor(x => x.SupplierId);
    }

    [Fact]
    public async Task Validate_WithEmptyProductId_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { ProductId = Guid.Empty });
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact]
    public async Task Validate_WithNegativePurchasePrice_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { PurchasePrice = -1m });
        result.ShouldHaveValidationErrorFor(x => x.PurchasePrice);
    }

    [Fact]
    public async Task Validate_WithZeroPurchasePrice_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { PurchasePrice = 0m });
        Assert.True(result.IsValid);
    }

    private static UpdateSupplierProductCommand CreateValidCommand()
    {
        return new UpdateSupplierProductCommand
        {
            SupplierId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            PurchasePrice = 5m,
            IsActive = true
        };
    }
}
