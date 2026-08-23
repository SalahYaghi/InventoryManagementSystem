using Contract.Features.Inventory.Adjustments.Queries.GetAdjustmentPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Adjustment.Queries.GetAdjustmentPaged;

public class GetAdjustmentPagedQueryValidatorTests
{
    private readonly GetAdjustmentPagedQueryValidator _validator = new();

    [Fact] public void Validate_WithValidPaging_ShouldNotHaveValidationError() { var q = new GetAdjustmentPagedQuery { PageNumber = 1, PageSize = 10 }; _validator.TestValidate(q).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithPageNumberLessThanMinimum_ShouldHaveValidationError() { var q = new GetAdjustmentPagedQuery { PageNumber = 0, PageSize = 10 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageNumber); }
    [Fact] public void Validate_WithPageSizeTooSmall_ShouldHaveValidationError() { var q = new GetAdjustmentPagedQuery { PageNumber = 1, PageSize = 0 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageSize); }
    [Fact] public void Validate_WithPageSizeTooLarge_ShouldHaveValidationError() { var q = new GetAdjustmentPagedQuery { PageNumber = 1, PageSize = 101 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageSize); }
}
