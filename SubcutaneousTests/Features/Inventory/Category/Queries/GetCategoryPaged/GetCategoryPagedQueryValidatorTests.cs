using Contract.Features.Inventory.Categories.Queries.GetCategoryPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Category.Queries.GetCategoryPaged;

public class GetCategoryPagedQueryValidatorTests
{
    private readonly GetCategoryPagedQueryValidator _validator = new();

    [Fact]
    public void Validate_WithDefaultQuery_ShouldNotHaveValidationError()
    {
        var query = new GetCategoryPagedQuery();
        _validator.TestValidate(query).ShouldNotHaveAnyValidationErrors();
    }
}
