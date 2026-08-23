using Contract.Features.Transactions.Orders.Commands.UpdateOrder;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Order.Commands.UpdateOrder;

public class UpdateOrderCommandValidatorTests
{
    private readonly UpdateOrderCommandValidator _validator = new();

    private static UpdateOrderCommand ValidCommand() => new()
    {
        Id = Guid.NewGuid(),
        DiscountAmount = 0m,
        Notes = "valid notes",
        DueDate = DateTimeOffset.UtcNow.AddDays(1)
    };

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
    public async Task Validate_WithNegativeDiscount_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand() with { DiscountAmount = -1m });
        result.ShouldHaveValidationErrorFor(x => x.DiscountAmount);
    }

    [Fact]
    public async Task Validate_WithNotesLongerThan500_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand() with { Notes = new string('a', 501) });
        result.ShouldHaveValidationErrorFor(x => x.Notes);
    }

    [Fact]
    public async Task Validate_WithExactly500CharacterNotes_ShouldNotHaveValidationErrorForNotes()
    {
        var result = await _validator.TestValidateAsync(ValidCommand() with { Notes = new string('a', 500) });
        result.ShouldNotHaveValidationErrorFor(x => x.Notes);
    }
}
