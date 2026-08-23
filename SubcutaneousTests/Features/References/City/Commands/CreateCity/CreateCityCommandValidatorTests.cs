using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.City.Commands.CreateCity;

public class CreateCityCommandValidatorTests
{
    private readonly global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommand { Id = Guid.NewGuid(), CountryId = Guid.NewGuid(), Name = "Gaza" };
        var result = await _validator.TestValidateAsync(command);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyId_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommand { Id = Guid.Empty, CountryId = Guid.NewGuid(), Name = "Gaza" };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Validate_WithEmptyName_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommand { Id = Guid.NewGuid(), CountryId = Guid.NewGuid(), Name = string.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Validate_WithTooLongName_ShouldHaveValidationError()
    {
        var command = new global::Contract.Features.References.Cities.Commands.CreateCity.CreateCityCommand { Id = Guid.NewGuid(), CountryId = Guid.NewGuid(), Name = new string('A', 101) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
