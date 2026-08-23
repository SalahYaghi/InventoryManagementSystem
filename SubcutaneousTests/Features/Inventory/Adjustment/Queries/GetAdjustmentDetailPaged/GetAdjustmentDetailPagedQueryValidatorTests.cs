using Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetailPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Adjustment.Queries.GetAdjustmentDetailPaged;

public class GetAdjustmentDetailPagedQueryValidatorTests
{
    private readonly GetAdjustmentDetailPagedQueryValidator _validator = new();

    [Fact]
    public void Validate_WithDefaultQuery_ShouldNotHaveValidationError()
    {
        var query = new GetAdjustmentDetailPagedQuery(Guid.NewGuid());
        _validator.TestValidate(query).ShouldNotHaveAnyValidationErrors();
    }
}
