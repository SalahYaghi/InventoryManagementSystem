using Domain.Customer;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using Xunit;

using CustomerEntity = Domain.Customer.Customer;

namespace InventoryManagement.Application.DomainTesting.Customers;

public class CustomerTests
{
    private static CustomerEntity ValidCustomer()
        => CustomerEntity.Create(
            Guid.NewGuid(), "Acme Ltd", "CUST-001",
            TestData.ValidContact(), TestData.ValidAddress(), "Good client").Value;

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var result = CustomerEntity.Create(
            Guid.NewGuid(), "Acme Ltd", "CUST-001",
            TestData.ValidContact(), TestData.ValidAddress(), null);

        Assert.True(result.IsSuccess);
        Assert.Equal("Acme Ltd", result.Value.CustomerName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithMissingName_Fails(string? name)
    {
        var result = CustomerEntity.Create(
            Guid.NewGuid(), name!, "CUST-001",
            TestData.ValidContact(), TestData.ValidAddress(), null);

        Assert.Equal(CustomerErrors.NameRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNameOver50Chars_Fails()
    {
        var result = CustomerEntity.Create(
            Guid.NewGuid(), new string('N', 51), "CUST-001",
            TestData.ValidContact(), TestData.ValidAddress(), null);

        Assert.Equal(CustomerErrors.NameTooLong.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithMissingCode_Fails(string? code)
    {
        var result = CustomerEntity.Create(
            Guid.NewGuid(), "Acme", code!,
            TestData.ValidContact(), TestData.ValidAddress(), null);

        Assert.Equal(CustomerErrors.CodeRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullContact_Fails()
    {
        var result = CustomerEntity.Create(
            Guid.NewGuid(), "Acme", "CUST-001", null, TestData.ValidAddress(), null);

        Assert.Equal(CustomerErrors.ContactRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullAddress_Fails()
    {
        var result = CustomerEntity.Create(
            Guid.NewGuid(), "Acme", "CUST-001", TestData.ValidContact(), null, null);

        Assert.Equal(CustomerErrors.AddressRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNotesOver500Chars_Fails()
    {
        var result = CustomerEntity.Create(
            Guid.NewGuid(), "Acme", "CUST-001",
            TestData.ValidContact(), TestData.ValidAddress(), new string('n', 501));

        Assert.Equal(CustomerErrors.NotesTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Update_WithValidData_ChangesBasicFields()
    {
        var customer = ValidCustomer();

        var result = customer.Update("New Name", "NEW-001", null, null, "new notes");

        Assert.True(result.IsSuccess);
        Assert.Equal("New Name", customer.CustomerName);
        Assert.Equal("NEW-001", customer.CustomerCode);
        Assert.Equal("new notes", customer.Notes);
    }

    [Fact]
    public void Update_WithNewContactValues_UpdatesContactInPlace()
    {
        var customer = ValidCustomer();
        var incoming = TestData.ValidContact(email: "new@example.com");

        var result = customer.Update("Acme Ltd", "CUST-001", incoming, null, null);

        Assert.True(result.IsSuccess);
        Assert.Equal("new@example.com", customer.Contact!.Email);
    }

    [Fact]
    public void Update_WithInvalidContact_ReturnsError()
    {
        var customer = ValidCustomer();
        var incoming = TestData.ValidContact();
        incoming.Email = "not-an-email"; // public setter allows this

        var result = customer.Update("Acme Ltd", "CUST-001", incoming, null, null);

        Assert.True(result.IsError);
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    //
    // Customer.Update calls this.Address.Update(...) and stores the result in
    // `updateResult` — but NEVER checks it. Contrast with the contact branch a
    // few lines below, which does check. An invalid address is silently
    // ignored and Update reports success. (Supplier.Update and Person.Update
    // have the identical bug — see SupplierTests / PersonTests.)
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Update_WithInvalidAddress_ShouldReturnError()
    {
        var customer = ValidCustomer();
        var badAddress = TestData.ValidAddress();
        badAddress.CountryId = Guid.Empty; // makes Address.Update fail

        var result = customer.Update("Acme Ltd", "CUST-001", null, badAddress, null);

        Assert.True(result.IsError); // FAILS: reports success today
    }
}
