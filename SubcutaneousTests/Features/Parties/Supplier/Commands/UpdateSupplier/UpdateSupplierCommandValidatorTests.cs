using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using FluentValidation.TestHelper;
using Xunit;

using Contract.Features.Parties.Supplier.Commands.UpdateSupplier;

namespace SubcutaneousTests.Features.Parties.Supplier.Commands.UpdateSupplier;

public class UpdateSupplierCommandValidatorTests
{
    private readonly UpdateSupplierCommandValidator _validator = new();

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
    public async Task Validate_WithEmptySupplierName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { SupplierName = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.SupplierName);
    }

    [Fact]
    public async Task Validate_WithTooLongSupplierName_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { SupplierName = new string('A', 51) });
        result.ShouldHaveValidationErrorFor(x => x.SupplierName);
    }

    [Fact]
    public async Task Validate_WithEmptySupplierCode_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { SupplierCode = string.Empty });
        result.ShouldHaveValidationErrorFor(x => x.SupplierCode);
    }

    [Fact]
    public async Task Validate_WithTooLongNotes_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(CreateValidCommand() with { Notes = new string('N', 501) });
        result.ShouldHaveValidationErrorFor(x => x.Notes);
    }

    private static UpdateSupplierCommand CreateValidCommand()
    {
        return new UpdateSupplierCommand
        {
            Id = Guid.NewGuid(),
            SupplierName = "Valid Supplier",
            SupplierCode = "SUP-VALID",
            Status = true,
            Contact = new UpdateContactInfoCommand { Id = Guid.NewGuid(), Email = "customer@test.com", PhoneNumber = "+970599999999", AlternitavePhoneNumber = "+970598888888", FaxNumber = "+9702222222", WebsiteUrl = "https://customer.example.com" },
            Address = new UpdateAddressCommand { Id = Guid.NewGuid(), CountryId = Guid.NewGuid(), CityId = Guid.NewGuid(), PostalCode = "12345", BuildingNumber = "10", Street = "Main Street", Description = "Valid address" },
            Notes = "valid notes"
        };
    }
}
