using Domain.Warehouses;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Warehouses;

public class WarehouseStockTests
{
    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var result = WarehouseStock.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            minimumStockLevel: 10m, quantity: 100m);

        Assert.True(result.IsSuccess);
        Assert.Equal(100m, result.Value.Quantity);
        Assert.Equal(10m, result.Value.MinimumStockLevel);
    }

    [Fact]
    public void Create_WithDefaultQuantity_IsZero()
    {
        var result = WarehouseStock.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), minimumStockLevel: 5m);

        Assert.True(result.IsSuccess);
        Assert.Equal(0m, result.Value.Quantity);
    }

    [Fact]
    public void Create_WithEmptyWarehouseId_Fails()
    {
        var result = WarehouseStock.Create(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), 5m);
        Assert.Equal(WarehouseStockErrors.WarehouseRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyProductId_Fails()
    {
        var result = WarehouseStock.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, 5m);
        Assert.Equal(WarehouseStockErrors.ProductRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNegativeMinimumLevel_Fails()
    {
        var result = WarehouseStock.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), -1m);
        Assert.Equal(WarehouseStockErrors.MinimumStockLevelInvalid.Code, result.TopError.Code);
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    //
    // Create() validates minimumStockLevel but NOT the initial quantity, so
    // stock can be created with NEGATIVE on-hand quantity. This breaks the
    // core inventory invariant (Quantity >= 0) that RemoveQuantity carefully
    // protects. Add: `if (quantity < 0) return WarehouseStockErrors.QuantityInvalid;`
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Create_WithNegativeQuantity_ShouldFail()
    {
        var result = WarehouseStock.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            minimumStockLevel: 5m, quantity: -50m);

        Assert.True(result.IsError); // succeeds today with Quantity == -50
    }

    // ---------------- AddToQuantity ----------------

    [Fact]
    public void AddToQuantity_WithPositiveAmount_IncreasesStock()
    {
        var stock = TestData.ValidWarehouseStock(quantity: 100m);

        var result = stock.AddToQuantity(25m);

        Assert.True(result.IsSuccess);
        Assert.Equal(125m, stock.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void AddToQuantity_WithZeroOrNegative_Fails(decimal amount)
    {
        var stock = TestData.ValidWarehouseStock(quantity: 100m);

        var result = stock.AddToQuantity(amount);

        Assert.Equal(WarehouseStockErrors.QuantityInvalid.Code, result.TopError.Code);
        Assert.Equal(100m, stock.Quantity);
    }

    // ---------------- RemoveQuantity ----------------

    [Fact]
    public void RemoveQuantity_WithValidAmount_DecreasesStock()
    {
        var stock = TestData.ValidWarehouseStock(quantity: 100m);

        var result = stock.RemoveQuantity(30m);

        Assert.True(result.IsSuccess);
        Assert.Equal(70m, stock.Quantity);
    }

    [Fact]
    public void RemoveQuantity_ExactlyAllStock_LeavesZero()
    {
        var stock = TestData.ValidWarehouseStock(quantity: 100m);

        var result = stock.RemoveQuantity(100m);

        Assert.True(result.IsSuccess);
        Assert.Equal(0m, stock.Quantity);
    }

    [Fact]
    public void RemoveQuantity_MoreThanAvailable_FailsAndDoesNotMutate()
    {
        var stock = TestData.ValidWarehouseStock(quantity: 100m);

        var result = stock.RemoveQuantity(100.01m);

        Assert.Equal(WarehouseStockErrors.QuantityExccededAlowedAmount.Code, result.TopError.Code);
        Assert.Equal(100m, stock.Quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void RemoveQuantity_WithZeroOrNegative_Fails(decimal amount)
    {
        var stock = TestData.ValidWarehouseStock(quantity: 100m);

        var result = stock.RemoveQuantity(amount);

        Assert.Equal(WarehouseStockErrors.QuantityInvalid.Code, result.TopError.Code);
    }

    // ---------------- UpdateMinimumLevel ----------------

    [Fact]
    public void UpdateMinimumLevel_WithValidValue_Changes()
    {
        var stock = TestData.ValidWarehouseStock(minimumStockLevel: 10m);

        var result = stock.UpdateMinimumLevel(20m);

        Assert.True(result.IsSuccess);
        Assert.Equal(20m, stock.MinimumStockLevel);
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    //
    // Create() rejects a negative minimum level, but UpdateMinimumLevel()
    // has no validation at all — the same invariant can be violated after
    // creation. Add the `minimumStockLevel < 0` check to Update as well.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void UpdateMinimumLevel_WithNegativeValue_ShouldFail()
    {
        var stock = TestData.ValidWarehouseStock(minimumStockLevel: 10m);

        var result = stock.UpdateMinimumLevel(-5m); // succeeds today

        Assert.True(result.IsError);
        Assert.Equal(10m, stock.MinimumStockLevel);
    }
}
