using Domain.Contacts.Address;
using Xunit;
using AddressEntity = Domain.Contacts.Address.Address;

namespace InventoryManagement.Application.DomainTesting.Contacts;

public class AddressTests
{
    private static AddressEntity CreateValid() =>
        AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            "12345", "10A", "Main St", "Near the market").Value!;

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var id = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var cityId = Guid.NewGuid();

        var result = AddressEntity.Create(id, countryId, cityId, "12345", "10A", "Main St", "desc");

        Assert.False(result.IsError);
        var address = result.Value!;
        Assert.Equal(id, address.Id);
        Assert.Equal(countryId, address.CountryId);
        Assert.Equal(cityId, address.CityId);
        Assert.Equal("12345", address.PostalCode);
        Assert.Equal("10A", address.BuildingNumber);
        Assert.Equal("Main St", address.Street);
        Assert.Equal("desc", address.Description);
    }

    [Fact]
    public void Create_WithEmptyCountryId_Fails()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), null, null, null, null);

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.CountryRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyCityId_Fails()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, null, null, null, null);

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.CityRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithAllOptionalFieldsNull_Succeeds()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, null);

        Assert.False(result.IsError);
    }

    [Fact]
    public void Create_WithPostalCodeOver20Chars_Fails()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new string('1', 21), null, null, null);

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.PostalCodeInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithBuildingNumberOver20Chars_Fails()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, new string('b', 21), null, null);

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.BuildingNumberInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithStreetOver20Chars_Fails()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, new string('s', 21), null);

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.StreetInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithDescriptionOver200Chars_Fails()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null, null, null, new string('d', 201));

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.DescriptionTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithBoundaryLengths_Succeeds()
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            new string('1', 20), new string('b', 20), new string('s', 20), new string('d', 200));

        Assert.False(result.IsError);
    }

    // ---------- Update ----------

    [Fact]
    public void Update_WithValidData_ReplacesAllFields()
    {
        var address = CreateValid();
        var newCountry = Guid.NewGuid();
        var newCity = Guid.NewGuid();

        var result = address.Update(newCountry, newCity, "54321", "7B", "Second St", "new desc");

        Assert.False(result.IsError);
        Assert.Equal(newCountry, address.CountryId);
        Assert.Equal(newCity, address.CityId);
        Assert.Equal("54321", address.PostalCode);
        Assert.Equal("7B", address.BuildingNumber);
        Assert.Equal("Second St", address.Street);
        Assert.Equal("new desc", address.Description);
    }

    [Fact]
    public void Update_WithEmptyCountryId_FailsWithoutMutating()
    {
        var address = CreateValid();
        var originalCity = address.CityId;

        var result = address.Update(Guid.Empty, Guid.NewGuid(), null, null, null, null);

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.CountryRequired.Code, result.TopError.Code);
        Assert.Equal(originalCity, address.CityId);
        Assert.Equal("12345", address.PostalCode);
    }

    [Fact]
    public void Update_WithStreetOver20Chars_Fails()
    {
        var address = CreateValid();

        var result = address.Update(
            Guid.NewGuid(), Guid.NewGuid(), null, null, new string('s', 21), null);

        Assert.True(result.IsError);
        Assert.Equal(AddressErrors.StreetInvalid.Code, result.TopError.Code);
    }

    // Design note (not a failing test): every property on Address has a public
    // setter, which is exactly what lets Customer/Supplier/Person callers hand
    // in an Address with CountryId = Guid.Empty. Private setters would close
    // that hole and make the swallowed-result bugs harder to hit.
}
