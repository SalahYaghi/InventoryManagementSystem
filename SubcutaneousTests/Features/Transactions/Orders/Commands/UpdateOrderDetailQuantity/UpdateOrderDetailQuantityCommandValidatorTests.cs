using Contract.Features.Transactions.Order.Commands.UpdateOrderDetail;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.UpdateOrderDetailQuantity;

public class UpdateOrderDetailQuantityCommandValidatorTests
{
    private readonly UpdateOrderDetailQuantityCommandValidator _validator = new();

    private static UpdateOrderDetailCommand ValidCommand() => new() { Id = Guid.NewGuid(), Quantity = 2m, RowVersion = [1, 2, 3, 4] };

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyId_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand() with { Id = Guid.Empty });
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Validate_WithZeroQuantity_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand() with { Quantity = 0m });
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public async Task Validate_WithNegativeQuantity_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand() with { Quantity = -1m });
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }
}
