using Contract.Features.Transactions.Order.Queries.GetOrderDetailPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Queries.GetOrderDetailPaged;

public class GetOrderDetailPagedQueryValidatorTests
{
    private readonly GetOrderDetailPagedQueryValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidOrderId_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetOrderDetailPagedQuery(Guid.NewGuid()));
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyOrderId_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new GetOrderDetailPagedQuery(Guid.Empty));
        result.ShouldHaveValidationErrorFor(x => x.OrderId);
    }
}
