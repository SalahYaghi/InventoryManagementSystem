using Contract.Features.Inventory.Product.Queries.GetProductPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Product.Queries.GetProductPaged;

public class GetProductPagedQueryValidatorTests
{
    private readonly GetProductPagedQueryValidator _validator = new();

    [Fact] public void Validate_WithValidPaging_ShouldNotHaveValidationError() { var q = new GetProductPagedQuery { PageNumber = 1, PageSize = 10 }; _validator.TestValidate(q).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithPageNumberLessThanMinimum_ShouldHaveValidationError() { var q = new GetProductPagedQuery { PageNumber = 0, PageSize = 10 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageNumber); }
    [Fact] public void Validate_WithPageSizeTooSmall_ShouldHaveValidationError() { var q = new GetProductPagedQuery { PageNumber = 1, PageSize = 0 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageSize); }
    [Fact] public void Validate_WithPageSizeTooLarge_ShouldHaveValidationError() { var q = new GetProductPagedQuery { PageNumber = 1, PageSize = 101 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageSize); }
}
