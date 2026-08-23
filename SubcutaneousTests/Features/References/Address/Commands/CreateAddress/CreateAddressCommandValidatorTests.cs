using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.Address.Commands.CreateAddress;

public class CreateAddressCommandValidatorTests
{
    private readonly global::Contract.Features.References.Addresses.Commands.CreateAddress.CreateAddressCommandValidator _validator = new();

    private static global::Contract.Features.References.Addresses.Commands.CreateAddress.CreateAddressCommand ValidCommand() => new()
    {
        CountryId = Guid.NewGuid(),
        CityId = Guid.NewGuid(),
        PostalCode = "12345",
        BuildingNumber = "10",
        Street = "Main Street",
        Description = "Valid description"
    };

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyCountryId_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { CountryId = Guid.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CountryId);
    }

    [Fact]
    public async Task Validate_WithEmptyCityId_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { CityId = Guid.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.CityId);
    }

    [Fact]
    public async Task Validate_WithTooLongPostalCode_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { PostalCode = new string('1', 21) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
    }

    [Fact]
    public async Task Validate_WithTooLongBuildingNumber_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { BuildingNumber = new string('1', 21) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.BuildingNumber);
    }

    [Fact]
    public async Task Validate_WithTooLongStreet_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Street = new string('S', 21) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Fact]
    public async Task Validate_WithTooLongDescription_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Description = new string('D', 201) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}
