using Contract.Features.Inventory.Warehouses.Commands.CreateWarehouse;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Warehouse.Commands.CreateWarehouse;

public class CreateWarehouseCommandValidatorTests
{
    private readonly CreateWarehouseCommandValidator _validator = new();

    private static CreateWarehouseCommand ValidCommand() => new()
    {
        Name = "Main Warehouse",
        Code = "WH-1",
        Address = new CreateAddressCommand { CountryId = Guid.NewGuid(), CityId = Guid.NewGuid(), Street = "Main Street" }
    };

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() => _validator.TestValidate(ValidCommand()).ShouldNotHaveAnyValidationErrors();
    [Fact] public void Validate_WithEmptyName_ShouldHaveValidationError() { var c = ValidCommand() with { Name = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Name); }
    [Fact] public void Validate_WithTooLongName_ShouldHaveValidationError() { var c = ValidCommand() with { Name = new string('W', 101) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Name); }
    [Fact] public void Validate_WithEmptyCode_ShouldHaveValidationError() { var c = ValidCommand() with { Code = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Code); }
    [Fact] public void Validate_WithTooLongCode_ShouldHaveValidationError() { var c = ValidCommand() with { Code = new string('C', 51) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Code); }
    [Fact] public void Validate_WithNullAddress_ShouldHaveValidationError() { var c = ValidCommand() with { Address = null! }; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.Address); }
}
