using Domain.People;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using DocumentEntity = Domain.Document.Document;
using DocumentTypeEnum = Domain.Document.DocumentType;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.People;

public class PersonTests
{
    private static Person CreateValidPerson()
    {
        var result = Person.Create(
            Guid.NewGuid(),
            nationalNo: "1234567890",
            firstName: "Ahmad",
            secondName: "Sami",
            thirdName: "Khalid",
            lastName: "Yousef",
            gender: true,
            dateOfBirth: new DateOnly(1990, 5, 1),
            contact: TestData.ValidContact(),
            address: TestData.ValidAddress());

        Assert.False(result.IsError);
        return result.Value!;
    }

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var person = CreateValidPerson();

        Assert.Equal("1234567890", person.NationalNo);
        Assert.Equal("Ahmad", person.FirstName);
        Assert.Equal("Yousef", person.LastName);
        Assert.NotNull(person.Contact);
        Assert.NotNull(person.Address);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithMissingNationalNo_Fails(string? nationalNo)
    {
        var result = Person.Create(
            Guid.NewGuid(), nationalNo!, "Ahmad", "Sami", null, "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.NationalNoRequired.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData("1234")]                     // 4 digits — below the 5-digit minimum
    [InlineData("123456789012345678901")]    // 21 digits — above the 20-digit maximum
    [InlineData("12345abc")]                 // non-digit characters
    public void Create_WithInvalidNationalNo_Fails(string nationalNo)
    {
        var result = Person.Create(
            Guid.NewGuid(), nationalNo, "Ahmad", "Sami", null, "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.NationalNoInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithFirstNameOver10Chars_Fails()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", new string('a', 11), "Sami", null, "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.FirstNameTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithSecondNameOver10Chars_Fails()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", new string('b', 11), null, "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.SecondNameTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithThirdNameOver10Chars_Fails()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", new string('c', 11), "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.ThirdNameTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullThirdName_Succeeds()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", null, "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_WithLastNameOver10Chars_Fails()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", null, new string('d', 11),
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.LastNameTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithFutureDateOfBirth_Fails()
    {
        var tomorrow = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", null, "Yousef",
            true, tomorrow, TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.DateOfBirthInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithTodayAsDateOfBirth_Succeeds()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", null, "Yousef",
            true, DateOnly.FromDateTime(DateTime.Today), TestData.ValidContact(), TestData.ValidAddress());

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_WithNullContact_Fails()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", null, "Yousef",
            true, new DateOnly(1990, 5, 1), null, TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.ContactRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullAddress_Fails()
    {
        var result = Person.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", null, "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), null);

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.AddressRequired.Code, result.TopError.Code);
    }

    // ⚠ DESIGN-NOTE TEST — FullName is FirstName + SecondName + LastName and
    // silently omits ThirdName even when present. If that's intentional
    // (common in some naming conventions) delete this test; if not, fix FullName.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void FullName_ShouldIncludeThirdName_WhenPresent()
    {
        var person = CreateValidPerson(); 
        Assert.Equal("Ahmad Sami Khalid Yousef", person.FullName);
    }

    // ---------- Update ----------

    [Fact]
    public void Update_WithValidData_UpdatesScalarsAndContactInPlace()
    {
        var person = CreateValidPerson();
        var originalContact = person.Contact;
        var newContact = TestData.ValidContact(email: "new@example.com", phone: "+972591111111");

        var result = person.Update(
            "9876543210", "Omar", "Ali", null, "Hassan",
            false, new DateOnly(1985, 1, 1), newContact, TestData.ValidAddress());

        Assert.False(result.IsError);
        Assert.Equal("9876543210", person.NationalNo);
        Assert.Equal("Omar", person.FirstName);
        Assert.Same(originalContact, person.Contact); // updated in place
        Assert.Equal("new@example.com", person.Contact!.Email);
    }

    [Fact]
    public void Update_WithInvalidNationalNo_FailsBeforeMutating()
    {
        var person = CreateValidPerson();

        var result = person.Update(
            "abc", "Omar", "Ali", null, "Hassan",
            false, new DateOnly(1985, 1, 1), TestData.ValidContact(), TestData.ValidAddress());

        Assert.True(result.IsError);
        Assert.Equal("1234567890", person.NationalNo);
        Assert.Equal("Ahmad", person.FirstName);
    }

    // ⚠ BUG-EXPOSING TEST — same defect as Customer.Update / Supplier.Update:
    // the result of Address.Update(...) is discarded, so an invalid address is
    // silently ignored and Update still reports success.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Update_WithInvalidAddress_ShouldFail_ButIsSilentlySwallowed()
    {
        var person = CreateValidPerson();

        var badAddress = TestData.ValidAddress();
        badAddress.CountryId = Guid.Empty; // Address.Update will reject this

        var result = person.Update(
            "1234567890", "Ahmad", "Sami", "Khalid", "Yousef",
            true, new DateOnly(1990, 5, 1), TestData.ValidContact(), badAddress);

        // EXPECTED: error. ACTUAL: success with the address unchanged.
        Assert.True(result.IsError);
    }
     [Fact]
     public void Update_WithInvalidContact_ShouldNotPartiallyMutate()
    {
        var person = CreateValidPerson();

        var badContact = TestData.ValidContact();
        badContact.Email = "not-an-email";  

        var result = person.Update(
            "9876543210", "Omar", "Ali", null, "Hassan",
            false, new DateOnly(1985, 1, 1), badContact, TestData.ValidAddress());

        Assert.True(result.IsError);

         Assert.Equal("Ahmad", person.FirstName);
    }

    // ---------- UpdateDocument / UpdateImageUrl ----------

    [Fact]
    public void UpdateDocument_WithValidDocument_SetsDocumentAndId()
    {
        var person = CreateValidPerson();
        var document = DocumentEntity.Create(
            Guid.NewGuid(), DocumentTypeEnum.Passport, "https://example.com/doc.png").Value!;

        var result = person.UpdateDocument(document);

        Assert.False(result.IsError);
        Assert.Same(document, person.Document);
        Assert.Equal(document.Id, person.DocumentId);
    }

    [Fact]
    public void UpdateDocument_WithNull_Fails()
    {
        var person = CreateValidPerson();

        var result = person.UpdateDocument(null!);

        Assert.True(result.IsError);
        Assert.Equal(PersonErrors.DocumentRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void UpdateImageUrl_AcceptsAnyValue()
    {
        // Note: UpdateImageUrl performs no validation at all, even though
        // PersonErrors.ImageUrlInvalid exists. Documented here as current behavior.
        var person = CreateValidPerson();

        var result = person.UpdateImageUrl("definitely not a url");

        Assert.False(result.IsError);
        Assert.Equal("definitely not a url", person.ImageUrl);
    }
}
