using Contract.Features.Parties.SupplierProducts.Queries.GetSupplierProductPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Parties.SupplierProduct.Queries.GetSupplierProductPaged;

public class GetSupplierProductPagedQueryValidatorTests
{
    private readonly GetSupplierProductPagedQueryValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidSupplierId_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetSupplierProductsPagedQuery(Guid.NewGuid()));
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptySupplierId_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetSupplierProductsPagedQuery(Guid.Empty));
        result.ShouldHaveValidationErrorFor(x => x.SupplierId);
    }
}
