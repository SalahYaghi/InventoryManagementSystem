using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.Country.Commands.CreateCountry;

public class CreateCountryCommandValidatorTests
{
    private readonly global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommand { Id = Guid.NewGuid(), Name = "Palestine" };
        var result = await _validator.TestValidateAsync(command);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyName_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommand { Id = Guid.NewGuid(), Name = string.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Validate_WithTooLongName_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.References.Countries.Commands.CreateCountry.CreateCountryCommand { Id = Guid.NewGuid(), Name = new string('A', 101) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
