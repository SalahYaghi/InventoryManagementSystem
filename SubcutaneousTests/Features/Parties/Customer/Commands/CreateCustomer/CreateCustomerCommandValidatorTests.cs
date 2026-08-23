using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using FluentValidation.TestHelper;
using Xunit;

using Contract.Features.Parties.Customers.Commands.CreateCustomer;

namespace SubcutaneousTests.Features.Parties.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandValidatorTests
{
    private readonly CreateCustomerCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand());
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyCustomerName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { CustomerName = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    [Fact]
    public async Task Validate_WithTooLongCustomerName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { CustomerName = new string('A', 51) });
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    [Fact]
    public async Task Validate_WithEmptyCustomerCode_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { CustomerCode = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.CustomerCode);
    }

    [Fact]
    public async Task Validate_WithTooLongCustomerCode_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { CustomerCode = new string('C', 51) });
        result.ShouldHaveValidationErrorFor(x => x.CustomerCode);
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

    [Fact]
    public async Task Validate_WithInvalidAddress_ShouldHaveValidationError()
    {
        var command = CreateValidCommand() with { Address = new CreateAddressCommand { CountryId = Guid.Empty, CityId = Guid.NewGuid(), Street = "Street" } };
        var result = await _validator.TestValidateAsync(command);
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithTooLongNotes_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { Notes = new string('N', 501) });
        result.ShouldHaveValidationErrorFor(x => x.Notes);
    }

    private static CreateCustomerCommand CreateValidCommand()
    {
        return new CreateCustomerCommand
        {
            CustomerName = "Valid Customer",
            CustomerCode = "CUS-VALID",
            Contact = new CreateContactInfoCommand
            {
                Email = "customer@test.com",
                PhoneNumber = "+970599999999",
                AlternitavePhoneNumber = "+970598888888",
                FaxNumber = "+9702222222",
                WebsiteUrl = "https://customer.example.com"
            },
            Address = new CreateAddressCommand
            {
                CountryId = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                PostalCode = "12345",
                BuildingNumber = "10",
                Street = "Main Street",
                Description = "Valid address"
            },
            Notes = "valid notes"
        };
    }
}
