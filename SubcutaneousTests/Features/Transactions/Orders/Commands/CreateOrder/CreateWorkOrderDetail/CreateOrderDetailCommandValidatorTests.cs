using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.CreateOrder.CreateWorkOrderDetail;

public class CreateOrderDetailCommandValidatorTests
{
    private readonly CreateOrderDetailCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new CreateOrderDetailCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 5m,
            RowVersion = [1, 2, 3, 4]
        };

        var result = await _validator.TestValidateAsync(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyProductId_ShouldHaveValidationError()
    {
        var command = new CreateOrderDetailCommand { ProductId = Guid.Empty, Quantity = 5m, RowVersion = [1, 2, 3, 4] };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact]
    public async Task Validate_WithZeroQuantity_ShouldHaveValidationError()
    {
        var command = new CreateOrderDetailCommand { ProductId = Guid.NewGuid(), Quantity = 0m, RowVersion = [1, 2, 3, 4] };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public async Task Validate_WithNegativeQuantity_ShouldHaveValidationError()
    {
        var command = new CreateOrderDetailCommand { ProductId = Guid.NewGuid(), Quantity = -1m, RowVersion = [1, 2, 3, 4] };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }
}
