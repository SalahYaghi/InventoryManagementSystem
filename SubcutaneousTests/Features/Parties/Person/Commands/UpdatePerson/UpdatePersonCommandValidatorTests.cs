using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using FluentValidation.TestHelper;
using Xunit;

using Contract.Features.Parties.People.Commands.UpdatePerson;

namespace SubcutaneousTests.Features.Parties.Person.Commands.UpdatePerson;

public class UpdatePersonCommandValidatorTests
{
    private readonly UpdatePersonCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyId_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { Id = Guid.Empty });
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public async Task Validate_WithEmptyNationalNo_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { NationalNo = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.NationalNo);
    }

    [Fact]
    public async Task Validate_WithTooLongFirstName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { FirstName = new string('F', 11) });
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public async Task Validate_WithEmptyLastName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { LastName = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    private static UpdatePersonCommand CreateValidCommand()
    {
        return new UpdatePersonCommand
        {
            Id = Guid.NewGuid(),
            NationalNo = $"NAT-{Guid.NewGuid():N}"[..12],
            FirstName = "Salah",
            SecondName = "Mazen",
            ThirdName = "Ali",
            LastName = "Ahmad",
            Gender = true,
            DateOfBirth = new DateOnly(2000, 1, 1),
            Contact = new UpdateContactInfoCommand { Id = Guid.NewGuid(), Email = "person@test.com", PhoneNumber = "+970599999999", AlternitavePhoneNumber = "+970598888888", FaxNumber = "+9702222222", WebsiteUrl = "https://person.example.com" },
            Address = new UpdateAddressCommand { Id = Guid.NewGuid(), CountryId = Guid.NewGuid(), CityId = Guid.NewGuid(), PostalCode = "12345", BuildingNumber = "10", Street = "Main Street", Description = "Valid address" }
        };
    }
}
