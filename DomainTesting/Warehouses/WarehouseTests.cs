using Domain.Warehouses;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Warehouses;

public class WarehouseTests
{
    [Fact]
    public void Create_WithValidData_SucceedsAndStartsActive()
    {
        var result = Warehouse.Create(Guid.NewGuid(), "Main WH", "WH-01", TestData.ValidAddress());

        Assert.True(result.IsSuccess);
        Assert.Equal(WarehouseStatus.Active, result.Value.WarehouseStatus);
        Assert.NotNull(result.Value.Address);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithMissingName_Fails(string? name)
    {
        var result = Warehouse.Create(Guid.NewGuid(), name!, "WH-01", TestData.ValidAddress());
        Assert.Equal(WarehouseErrors.NameRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNameOver100Chars_Fails()
    {
        var result = Warehouse.Create(Guid.NewGuid(), new string('W', 101), "WH-01", TestData.ValidAddress());
        Assert.Equal(WarehouseErrors.NameTooLong.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithMissingCode_Fails(string? code)
    {
        var result = Warehouse.Create(Guid.NewGuid(), "Main", code!, TestData.ValidAddress());
        Assert.Equal(WarehouseErrors.CodeRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithCodeOver50Chars_Fails()
    {
        var result = Warehouse.Create(Guid.NewGuid(), "Main", new string('C', 51), TestData.ValidAddress());
        Assert.Equal(WarehouseErrors.CodeTooLong.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNullAddress_Fails()
    {
        var result = Warehouse.Create(Guid.NewGuid(), "Main", "WH-01", null);
        Assert.Equal(WarehouseErrors.AddressRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Update_WithValidData_ChangesFields()
    {
        var warehouse = Warehouse.Create(Guid.NewGuid(), "Old", "OLD-1", TestData.ValidAddress()).Value;

        var result = warehouse.Update("New Name", "NEW-1", null, WarehouseStatus.Inactive);

        Assert.True(result.IsSuccess);
        Assert.Equal("New Name", warehouse.Name);
        Assert.Equal("NEW-1", warehouse.Code);
        Assert.Equal(WarehouseStatus.Inactive, warehouse.WarehouseStatus);
    }

    [Fact]
    public void Update_WithUndefinedStatus_Fails()
    {
        var warehouse = Warehouse.Create(Guid.NewGuid(), "Main", "WH-01", TestData.ValidAddress()).Value;

        var result = warehouse.Update("Main", "WH-01", null, (WarehouseStatus)99);

        Assert.Equal(WarehouseErrors.InvalidStatus.Code, result.TopError.Code);
    }

    [Fact]
    public void Update_WithNewAddressValues_UpdatesExistingAddressInPlace()
    {
        var warehouse = Warehouse.Create(Guid.NewGuid(), "Main", "WH-01", TestData.ValidAddress()).Value;
        var incoming = TestData.ValidAddress();
        incoming.Street = "Changed St"; // Address properties have public setters

        var result = warehouse.Update("Main", "WH-01", incoming, WarehouseStatus.Active);

        Assert.True(result.IsSuccess);
        Assert.Equal("Changed St", warehouse.Address!.Street);
    }
 
    [Fact]
    public void Update_WhenAddressUpdateFails_ShouldNotMutateAnything()
    {
        var warehouse = Warehouse.Create(Guid.NewGuid(), "Original", "ORIG-1", TestData.ValidAddress()).Value;

        var badAddress = TestData.ValidAddress();
        badAddress.CountryId = Guid.Empty;  

        var result = warehouse.Update("Changed", "CHG-1", badAddress, WarehouseStatus.Inactive);

        Assert.True(result.IsError);
        Assert.Equal("Original", warehouse.Name);   
        Assert.Equal("ORIG-1", warehouse.Code);    
    }
}
