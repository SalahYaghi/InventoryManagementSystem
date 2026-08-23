using Domain.Suppliers;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using AddressEntity = Domain.Contacts.Address.Address;
using ContactInfoEntity = Domain.Contacts.ContactInfo.ContactInfo;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Suppliers;

public class SupplierTests
{
    private static Supplier CreateValidSupplier(
        ContactInfoEntity? contact = null,
        AddressEntity? address = null)
    {
        var result = Supplier.Create(
            Guid.NewGuid(),
            "Acme Supplies",
            "SUP-001",
            contact ?? TestData.ValidContact(),
            address ?? TestData.ValidAddress(),
            status: true,
            notes: "preferred vendor");

        Assert.False(result.IsError);
        return result.Value!;
    }

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var id = Guid.NewGuid();
        var contact = TestData.ValidContact();
        var address = TestData.ValidAddress();

        var result = Supplier.Create(id, "Acme Supplies", "SUP-001", contact, address, true, "notes");

        Assert.False(result.IsError);
        var supplier = result.Value!;
        Assert.Equal(id, supplier.Id);
        Assert.Equal("Acme Supplies", supplier.SupplierName);
        Assert.Equal("SUP-001", supplier.SupplierCode);
        Assert.Same(contact, supplier.Contact);
        Assert.Same(address, supplier.Address);
        Assert.True(supplier.Status);
        Assert.Equal("notes", supplier.Notes);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithMissingName_Fails(string? name)
    {
        var result = Supplier.Create(
            Guid.NewGuid(), name!, "SUP-001",
            TestData.ValidContact(), TestData.ValidAddress(), true, null);

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.NameRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNameOver50Chars_Fails()
    {
        var result = Supplier.Create(
            Guid.NewGuid(), new string('a', 51), "SUP-001",
            TestData.ValidContact(), TestData.ValidAddress(), true, null);

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.NameTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNameExactly50Chars_Succeeds()
    {
        var result = Supplier.Create(
            Guid.NewGuid(), new string('a', 50), "SUP-001",
            TestData.ValidContact(), TestData.ValidAddress(), true, null);

        Assert.False(result.IsError);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithMissingCode_Fails(string? code)
    {
        var result = Supplier.Create(
            Guid.NewGuid(), "Acme", code!,
            TestData.ValidContact(), TestData.ValidAddress(), true, null);

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.CodeRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithCodeOver50Chars_Fails()
    {
        var result = Supplier.Create(
            Guid.NewGuid(), "Acme", new string('c', 51),
            TestData.ValidContact(), TestData.ValidAddress(), true, null);

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.CodeTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullContact_Fails()
    {
        var result = Supplier.Create(
            Guid.NewGuid(), "Acme", "SUP-001",
            contact: null, TestData.ValidAddress(), true, null);

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.ContactRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullAddress_Fails()
    {
        var result = Supplier.Create(
            Guid.NewGuid(), "Acme", "SUP-001",
            TestData.ValidContact(), address: null, true, null);

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.AddressRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNotesOver500Chars_Fails()
    {
        var result = Supplier.Create(
            Guid.NewGuid(), "Acme", "SUP-001",
            TestData.ValidContact(), TestData.ValidAddress(), true, new string('n', 501));

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.NotesTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullNotes_Succeeds()
    {
        var result = Supplier.Create(
            Guid.NewGuid(), "Acme", "SUP-001",
            TestData.ValidContact(), TestData.ValidAddress(), false, null);

        Assert.False(result.IsError);
        Assert.Null(result.Value!.Notes);
        Assert.False(result.Value!.Status);
    }

    // ---------- Update ----------

    [Fact]
    public void Update_WithValidData_UpdatesScalarsAndContactInPlace()
    {
        var supplier = CreateValidSupplier();
        var newContact = TestData.ValidContact(email: "new@example.com", phone: "+972591111111");
        var originalContact = supplier.Contact;

        var result = supplier.Update(
            "New Name", "SUP-002", newContact, TestData.ValidAddress(), false, "updated");

        Assert.False(result.IsError);
        Assert.Equal("New Name", supplier.SupplierName);
        Assert.Equal("SUP-002", supplier.SupplierCode);
        Assert.False(supplier.Status);
        Assert.Equal("updated", supplier.Notes);

        // The existing contact instance is updated in place (not replaced).
        Assert.Same(originalContact, supplier.Contact);
        Assert.Equal("new@example.com", supplier.Contact!.Email);
    }

    [Fact]
    public void Update_WithInvalidName_FailsBeforeMutating()
    {
        var supplier = CreateValidSupplier();

        var result = supplier.Update(
            "", "SUP-002", TestData.ValidContact(), TestData.ValidAddress(), false, null);

        Assert.True(result.IsError);
        Assert.Equal(SupplierErrors.NameRequired.Code, result.TopError.Code);
        Assert.Equal("Acme Supplies", supplier.SupplierName);
    }

    // ⚠ BUG-EXPOSING TEST — same defect as Customer.Update / Person.Update:
    // the result of Address.Update(...) is assigned to a local variable and
    // never checked, so an invalid address is silently swallowed and the
    // method still returns success. (The contact result IS checked.)
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Update_WithInvalidAddress_ShouldFail_ButIsSilentlySwallowed()
    {
        var supplier = CreateValidSupplier();

        // Address has public setters, so we can hand Update() an address
        // carrying an invalid CountryId that Address.Update will reject.
        var badAddress = TestData.ValidAddress();
        badAddress.CountryId = Guid.Empty;

        var result = supplier.Update(
            "Acme Supplies", "SUP-001", TestData.ValidContact(), badAddress, true, null);

        // EXPECTED (correct behavior): the update fails.
        // ACTUAL (current code): result is success and the address keeps its old values.
        Assert.True(result.IsError);
    }
 
    [Fact]
     public void Update_WithInvalidContact_ShouldNotPartiallyMutate()
    {
        var supplier = CreateValidSupplier();

        var badContact = TestData.ValidContact();
        badContact.Email = "not-an-email";  

        var result = supplier.Update(
            "Mutated Name", "MUT-999", badContact, TestData.ValidAddress(), false, "mutated");

        Assert.True(result.IsError);

        
        Assert.Equal("Acme Supplies", supplier.SupplierName);
    }
}
