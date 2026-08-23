using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.References.ContactInfo.Commands.UpdateContactInfo;

public class UpdateContactInfoCommandValidatorTests
{
    private readonly global::Contract.Features.References.ContactInfos.Commands.UpdateContactInfo.UpdateContactInfoCommandValidator _validator = new();

    private static global::Contract.Features.References.ContactInfos.Commands.UpdateContactInfo.UpdateContactInfoCommand ValidCommand() => new()
    {
        Id = Guid.NewGuid(),
        Email = "person@test.com",
        PhoneNumber = "+970599123456",
        AlternitavePhoneNumber = "+970598123456",
        FaxNumber = "+970222222222",
        WebsiteUrl = "https://example.com"
    };

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(ValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyEmail_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Email = string.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Validate_WithTooLongEmail_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { Email = new string('a', 257) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Validate_WithEmptyPhoneNumber_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { PhoneNumber = string.Empty };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
    }

    [Fact]
    public async Task Validate_WithTooLongWebsiteUrl_ShouldHaveValidationError()
    {
        var command = ValidCommand() with { WebsiteUrl = new string('w', 501) };
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
    }
}
