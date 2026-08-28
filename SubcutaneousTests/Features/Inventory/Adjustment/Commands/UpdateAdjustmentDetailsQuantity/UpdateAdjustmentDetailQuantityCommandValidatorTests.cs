using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustment;
using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustmentDetailsQuantity;
using Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail;
using Contract.Features.Inventory.Adjustments.Commands.CreateAdjustment;
using Contract.Features.Inventory.Adjustments.Commands.UpdateAdjustment;
using Contract.Features.Transactions.Order.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Order.Commands.UpdateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.UpdateOrder;
using Domain.Adjustments;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Adjustment.Commands.UpdateAdjustmentDetailsQuantity;

public class CreateAdjustmentCommandValidatorTests
{
    private readonly CreateAdjustmentCommandValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var c = new CreateAdjustmentDetailInnerCommand { ProductId = Guid.NewGuid(), Quantity = 1m, RowVersion = [1, 2, 3] };
        var command = new CreateAdjustmentCommand { WarehouseId = Guid.NewGuid(), AdjustmentReason = AdjustmentReason.ExtraFound, Notes = "Valid" , 
        AdjustmentDetailCommands = new() {
           c
        }
        };
        _validator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyWarehouseId_ShouldHaveValidationError()
    {
        var command = new CreateAdjustmentCommand { WarehouseId = Guid.Empty, AdjustmentReason = AdjustmentReason.ExtraFound };
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(x => x.WarehouseId);
    }

    [Fact]
    public void Validate_WithTooLongNotes_ShouldHaveValidationError()
    {
        var command = new CreateAdjustmentCommand { WarehouseId = Guid.NewGuid(), AdjustmentReason = AdjustmentReason.ExtraFound, Notes = new string('N', 501) };
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(x => x.Notes);
    }
}

public class CreateAdjustmentDetailInnerCommandValidatorTests
{
    private readonly CreateAdjustmentDetailInnerCommandValidator _validator = new();

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() { var c = new CreateAdjustmentDetailInnerCommand { ProductId = Guid.NewGuid(), Quantity = 1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithEmptyProductId_ShouldHaveValidationError() { var c = new CreateAdjustmentDetailInnerCommand { ProductId = Guid.Empty, Quantity = 1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.ProductId); }
    [Fact] public void Validate_WithZeroQuantity_ShouldHaveValidationError() { var c = new CreateAdjustmentDetailInnerCommand { ProductId = Guid.NewGuid(), Quantity = 0m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Quantity); }
    [Fact] public void Validate_WithNegativeQuantity_ShouldHaveValidationError() { var c = new CreateAdjustmentDetailInnerCommand { ProductId = Guid.NewGuid(), Quantity = -1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Quantity); }
}

public class CreateAdjustmentDetailCommandValidatorTests
{
    private readonly CreateAdjustmentDetailCommandValidator _validator = new();

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() { var c = new CreateAdjustmentDetailCommand { AdjustmentId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithEmptyAdjustmentId_ShouldHaveValidationError() { var c = new CreateAdjustmentDetailCommand { AdjustmentId = Guid.Empty, ProductId = Guid.NewGuid(), Quantity = 1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.AdjustmentId); }
    [Fact] public void Validate_WithEmptyProductId_ShouldHaveValidationError() { var c = new CreateAdjustmentDetailCommand { AdjustmentId = Guid.NewGuid(), ProductId = Guid.Empty, Quantity = 1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.ProductId); }
    [Fact] public void Validate_WithZeroQuantity_ShouldHaveValidationError() { var c = new CreateAdjustmentDetailCommand { AdjustmentId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 0m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Quantity); }
}

public class UpdateAdjustmentCommandValidatorTests
{
    private readonly UpdateAdjustmentCommandValidator _validator = new();

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() { var c = new Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustment.UpdateAdjustmentCommand { Id = Guid.NewGuid(), Notes = "Valid" }; _validator.TestValidate(c).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithEmptyId_ShouldHaveValidationError() { var c = new UpdateAdjustmentCommand { Id = Guid.Empty, Notes = "Valid" }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Id); }
    [Fact] public void Validate_WithTooLongNotes_ShouldHaveValidationError() { var c = new UpdateAdjustmentCommand { Id = Guid.NewGuid(), Notes = new string('N', 501) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Notes); }
}

public class UpdateAdjustmentDetailQuantityCommandValidatorTests
{
    private readonly UpdateAdjustmentDetailQuantityCommandValidator _validator = new();

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() { var c = new UpdateAdjustmentDetailQuantityCommand { Id = Guid.NewGuid(), Quantity = 1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithEmptyId_ShouldHaveValidationError() { var c = new UpdateAdjustmentDetailQuantityCommand { Id = Guid.Empty, Quantity = 1m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Id); }
    [Fact] public void Validate_WithZeroQuantity_ShouldHaveValidationError() { var c = new UpdateAdjustmentDetailQuantityCommand { Id = Guid.NewGuid(), Quantity = 0m, RowVersion = [1, 2, 3] }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Quantity); }
}

public class UpdateAdjustmentValidatorTests
{
    private readonly UpdateAdjustmentValidator _validator = new();

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() { var c = new UpdateAdjustmentStatusCommand { Id = Guid.NewGuid(), AdjustmentStatus = AdjustmentStatus.Approved }; _validator.TestValidate(c).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithEmptyId_ShouldHaveValidationError() { var c = new UpdateAdjustmentStatusCommand { Id = Guid.Empty, AdjustmentStatus = AdjustmentStatus.Approved }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Id); }
}
