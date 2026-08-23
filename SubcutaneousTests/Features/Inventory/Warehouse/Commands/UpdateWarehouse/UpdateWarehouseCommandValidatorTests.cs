using Contract.Features.Inventory.Warehouses.Commands.UpdateWarehouse;
using Domain.Warehouses;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Warehouse.Commands.UpdateWarehouse;

public class UpdateWarehouseCommandValidatorTests
{
    private readonly UpdateWarehouseCommandValidator _validator = new();

    private static UpdateWarehouseCommand ValidCommand() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Main Warehouse",
        Code = "WH-1",
        WarehouseStatus = WarehouseStatus.Active
    };

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() => _validator.TestValidate(ValidCommand()).ShouldNotHaveAnyValidationErrors();
    [Fact] public void Validate_WithEmptyId_ShouldHaveValidationError() { var c = ValidCommand() with { Id = Guid.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Id); }
    [Fact] public void Validate_WithEmptyName_ShouldHaveValidationError() { var c = ValidCommand() with { Name = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Name); }
    [Fact] public void Validate_WithTooLongName_ShouldHaveValidationError() { var c = ValidCommand() with { Name = new string('W', 101) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Name); }
    [Fact] public void Validate_WithEmptyCode_ShouldHaveValidationError() { var c = ValidCommand() with { Code = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Code); }
    [Fact] public void Validate_WithTooLongCode_ShouldHaveValidationError() { var c = ValidCommand() with { Code = new string('C', 51) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Code); }
}
