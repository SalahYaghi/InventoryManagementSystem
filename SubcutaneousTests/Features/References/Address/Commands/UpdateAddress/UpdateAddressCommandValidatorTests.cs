using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.Address.Commands.UpdateAddress;

public class UpdateAddressCommandValidatorTests
{
    private readonly global::Contract.Features.References.Addresses.Commands.UpdateAddress.UpdateAddressCommandValidator _validator = new();

    private static global::Contract.Features.References.Addresses.Commands.UpdateAddress.UpdateAddressCommand ValidCommand() => new()
    {
        Id = Guid.NewGuid(),
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
    public async Task Validate_WithTooLongDescription_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Description = new string('D', 201) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}
