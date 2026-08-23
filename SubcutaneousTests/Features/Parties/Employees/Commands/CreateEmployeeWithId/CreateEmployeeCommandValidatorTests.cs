using Contract.Features.Parties.Employees.Commands.CreateEmployeeWithId;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Parties.Employees.Commands.CreateEmployeeWithId;

public class CreateEmployeeCommandValidatorTests
{
    private readonly CreateEmployeeCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new CreateEmployeeWithPersonIdCommand("Manager", Guid.NewGuid(), new DateOnly(2024, 1, 1), Guid.NewGuid());
        var result = await _validator.TestValidateAsync(command);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyJobTitle_ShouldHaveValidationError()
    {
        var command = new CreateEmployeeWithPersonIdCommand(string.Empty, Guid.NewGuid(), new DateOnly(2024, 1, 1), Guid.NewGuid());
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.jobTitle);
    }

    [Fact]
    public async Task Validate_WithEmptyPersonId_ShouldHaveValidationError()
    {
        var command = new CreateEmployeeWithPersonIdCommand("Manager", Guid.Empty, new DateOnly(2024, 1, 1), Guid.NewGuid());
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.personId);
    }
}
