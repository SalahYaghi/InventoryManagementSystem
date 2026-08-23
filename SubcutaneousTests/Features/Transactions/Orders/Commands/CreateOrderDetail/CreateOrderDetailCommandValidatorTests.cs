using Contract.Features.Transactions.Order.Commands.CreateOrderDetail;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.CreateOrderDetail;

public class CreateOrderDetailCommandValidatorTests
{
    private readonly CreateOrderDetailCommandValidator _validator = new();

    private static CreateOrderDetailCommand ValidCommand() => new()
    {
        OrderId = Guid.NewGuid(),
        ProductId = Guid.NewGuid(),
        Quantity = 5m,
        RowVersion = [1, 2, 3, 4]
    };

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyOrderId_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { OrderId = Guid.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.OrderId);
    }

    [Fact]
    public async Task Validate_WithEmptyProductId_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { ProductId = Guid.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact]
    public async Task Validate_WithZeroQuantity_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Quantity = 0m };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public async Task Validate_WithNegativeQuantity_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Quantity = -1m };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }
}
