using Contract.Common.Constants;
using Contract.Features.Transactions.Orders.Queries.GetOrderPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Queries.GetOrderPaged;

public class GetOrderPagedQueryValidatorTests
{
    private readonly GetOrderPagedQueryValidator _validator = new();

    [Fact]
    public async Task Validate_WithDefaultValues_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetOrderPagedQuery());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithPageNumberBelowMinimum_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetOrderPagedQuery { PageNumber = ApplicationDefaults.DefaultPageNumber - 1, PageSize = ApplicationDefaults.DefaultPageSize });
        result.ShouldHaveValidationErrorFor(x => x.PageNumber);
    }

    [Fact]
    public async Task Validate_WithPageSizeBelowMinimum_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetOrderPagedQuery { PageNumber = 1, PageSize = ApplicationDefaults.MinimumPageSize - 1 });
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }

    [Fact]
    public async Task Validate_WithPageSizeAboveMaximum_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetOrderPagedQuery { PageNumber = 1, PageSize = ApplicationDefaults.MaximumPageSize + 1 });
        result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }
}
