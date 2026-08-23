using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Domain.Orders;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.CreateOrder;

public class CreateOrderCommandValidatorTests
{
    private readonly CreateOrderCommandValidator _validator = new();

    private static CreateOrderCommand ValidCommand() => new()
    {
        SupplierId = Guid.NewGuid(),
        SourceWarehouseId = Guid.NewGuid(),
        OrderType = OrderType.Purchase,
        DueDate = DateTimeOffset.UtcNow.AddDays(1),
        Notes = "valid notes",
        OrderDetails =
        [
            new CreateOrderDetailCommand
            {
                ProductId = Guid.NewGuid(),
                Quantity = 2m,
                RowVersion = [1, 2, 3, 4]
            }
        ]
    };

    [Fact]
    public async Task Validate_WithValidPurchaseCommand_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptySourceWarehouseId_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { SourceWarehouseId = Guid.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.SourceWarehouseId);
    }

    [Fact]
    public async Task Validate_WithNotesLongerThan500_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Notes = new string('a', 501) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Notes);
    }

    [Fact]
    public async Task Validate_WithEmptyOrderType_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { OrderType = 0 };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.OrderType);
    }

    [Fact]
    public async Task Validate_WithEmptyDetailProductId_ShouldHaveNestedValidationError()
    {
        var command = ValidCommand();
        command.OrderDetails[0] = command.OrderDetails[0] with { ProductId = Guid.Empty };
        var result = await _validator.TestValidateAsync(command);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains(nameof(CreateOrderDetailCommand.ProductId)));
    }

    [Fact]
    public async Task Validate_WithZeroDetailQuantity_ShouldHaveNestedValidationError()
    {
        var command = ValidCommand();
        command.OrderDetails[0] = command.OrderDetails[0] with { Quantity = 0m };
        var result = await _validator.TestValidateAsync(command);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains(nameof(CreateOrderDetailCommand.Quantity)));
    }

    [Fact]
    public async Task Validate_WithNegativeDetailQuantity_ShouldHaveNestedValidationError()
    {
        var command = ValidCommand();
        command.OrderDetails[0] = command.OrderDetails[0] with { Quantity = -1m };
        var result = await _validator.TestValidateAsync(command);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains(nameof(CreateOrderDetailCommand.Quantity)));
    }

    [Fact]
    public async Task Validate_WithExactly500CharacterNotes_ShouldNotHaveValidationErrorForNotes()
    {
        var command = ValidCommand() with { Notes = new string('a', 500) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Notes);
    }
}
