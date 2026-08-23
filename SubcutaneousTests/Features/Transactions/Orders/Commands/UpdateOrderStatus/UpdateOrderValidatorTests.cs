using Contract.Features.Transactions.Orders.Commands.UpdateOrder;
using Domain.Orders;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.UpdateOrderStatus;

public class UpdateOrderValidatorTests
{
    private readonly UpdateOrderValidator _validator = new();

    private static UpdateOrderStatusCommand ValidCommand() => new() { Id = Guid.NewGuid(), OrderStatus = OrderStatus.Completed };

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
    public async Task Validate_WithEmptyOrderStatus_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand() with { OrderStatus = 0 });
        result.ShouldHaveValidationErrorFor(x => x.OrderStatus);
    }
}
