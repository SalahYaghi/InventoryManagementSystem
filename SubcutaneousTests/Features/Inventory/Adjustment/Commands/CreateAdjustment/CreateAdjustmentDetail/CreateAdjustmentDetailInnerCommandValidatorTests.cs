using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Adjustment.Commands.CreateAdjustment.CreateAdjustmentDetail;

public class CreateAdjustmentDetailInnerCommandValidatorTests
{
    private readonly global::Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail.CreateAdjustmentDetailInnerCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithEmptyOrDefaultData_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail.CreateAdjustmentDetailInnerCommand
        {
            ProductId = Guid.Empty,
            Quantity = 0m,
            RowVersion = Array.Empty<byte>()
        };

        var result = await _validator.TestValidateAsync(command);

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new global::Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail.CreateAdjustmentDetailInnerCommand
        {
            ProductId = Guid.NewGuid(),
            Quantity = 5m,
            RowVersion = new byte[] { 1, 2, 3, 4 }
        };

        var result = await _validator.TestValidateAsync(command);

        Assert.True(result.IsValid);
    }
}
