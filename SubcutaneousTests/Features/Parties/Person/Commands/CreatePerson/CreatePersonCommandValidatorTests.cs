using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using FluentValidation.TestHelper;
using Xunit;

using Contract.Features.Parties.People.Commands.CreatePerson;

namespace SubcutaneousTests.Features.Parties.Person.Commands.CreatePerson;

public class CreatePersonCommandValidatorTests
{
    private readonly CreatePersonCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyNationalNo_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { NationalNo = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.NationalNo);
    }

    [Fact]
    public async Task Validate_WithTooLongNationalNo_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { NationalNo = new string('N', 21) });
        result.ShouldHaveValidationErrorFor(x => x.NationalNo);
    }

    [Fact]
    public async Task Validate_WithEmptyFirstName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { FirstName = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public async Task Validate_WithTooLongFirstName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { FirstName = new string('F', 11) });
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public async Task Validate_WithEmptySecondName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { SecondName = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.SecondName);
    }

    [Fact]
    public async Task Validate_WithTooLongThirdName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { ThirdName = new string('T', 11) });
        result.ShouldHaveValidationErrorFor(x => x.ThirdName);
    }

    [Fact]
    public async Task Validate_WithEmptyLastName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { LastName = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public async Task Validate_WithNullContact_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { Contact = null! });
        result.ShouldHaveValidationErrorFor(x => x.Contact);
    }

    [Fact]
    public async Task Validate_WithNullAddress_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { Address = null! });
        result.ShouldHaveValidationErrorFor(x => x.Address);
    }

    private static CreatePersonCommand CreateValidCommand()
    {
        return new CreatePersonCommand
        {
            NationalNo = $"NAT-{Guid.NewGuid():N}"[..12],
            FirstName = "Salah",
            SecondName = "Mazen",
            ThirdName = "Ali",
            LastName = "Ahmad",
            Gender = true,
            DateOfBirth = new DateOnly(2000, 1, 1),
            Contact = new CreateContactInfoCommand { Email = "person@test.com", PhoneNumber = "+970599999999", AlternitavePhoneNumber = "+970598888888", FaxNumber = "+9702222222", WebsiteUrl = "https://person.example.com" },
            Address = new CreateAddressCommand { CountryId = Guid.NewGuid(), CityId = Guid.NewGuid(), PostalCode = "12345", BuildingNumber = "10", Street = "Main Street", Description = "Valid address" }
        };
    }
}
