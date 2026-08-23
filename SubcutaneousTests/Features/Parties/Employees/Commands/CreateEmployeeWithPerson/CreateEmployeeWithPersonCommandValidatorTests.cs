using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using FluentValidation.TestHelper;
using Xunit;

using Contract.Features.Parties.Employees.Commands.CreateEmployeeWithPerson;
using Contract.Features.Parties.People.Commands.CreatePerson;

namespace SubcutaneousTests.Features.Parties.Employees.Commands.CreateEmployeeWithPerson;

public class CreateEmployeeWithPersonCommandValidatorTests
{
    private readonly CreateEmployeeWithPersonCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new CreateEmployeeWithPersonCommand("Manager", CreatePerson(), new DateOnly(2024, 1, 1), Guid.NewGuid());
        var result = await _validator.TestValidateAsync(command);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyJobTitle_ShouldHaveValidationError()
    {
        var command = new CreateEmployeeWithPersonCommand(string.Empty, CreatePerson(), new DateOnly(2024, 1, 1), Guid.NewGuid());
        var result = await _validator.TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor(x => x.jobTitle);
    }

    private static CreatePersonCommand CreatePerson()
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
