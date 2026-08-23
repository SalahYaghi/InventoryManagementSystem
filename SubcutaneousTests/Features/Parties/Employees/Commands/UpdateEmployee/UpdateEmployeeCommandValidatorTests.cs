using Contract.Features.Parties.Employees.Commands.UpdateEmployee;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Parties.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidatorTests
{
    private readonly UpdateEmployeeCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new UpdateEmployeeCommand(Guid.NewGuid(), "Manager", new DateOnly(2024, 1, 1), Guid.NewGuid());
        var result = await _validator.TestValidateAsync(command);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyJobTitle_ShouldHaveValidationError()
    {
        var command = new UpdateEmployeeCommand(Guid.NewGuid(), string.Empty, new DateOnly(2024, 1, 1), Guid.NewGuid());
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.jobTitle);
    }

    [Fact]
    public async Task Validate_WithEmptyWarehouseId_ShouldHaveValidationError()
    {
        var command = new UpdateEmployeeCommand(Guid.NewGuid(), "Manager", new DateOnly(2024, 1, 1), Guid.Empty);
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.warehouseId);
    }
}
