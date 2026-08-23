using Domain.Suppliers.SupplierProducts;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Suppliers;

public class SupplierProductTests
{
    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_SucceedsAndDefaultsToActive()
    {
        var id = Guid.NewGuid();
        var supplierId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var result = SupplierProduct.Create(id, supplierId, productId, 12.5m);

        Assert.False(result.IsError);
        var sp = result.Value!;
        Assert.Equal(id, sp.Id);
        Assert.Equal(supplierId, sp.SupplierId);
        Assert.Equal(productId, sp.ProductId);
        Assert.Equal(12.5m, sp.PurchasePrice);

        // Create hard-codes IsActive = true; there is no way to create an inactive link.
        Assert.True(sp.IsActive);
    }

    [Fact]
    public void Create_WithEmptySupplierId_Fails()
    {
        var result = SupplierProduct.Create(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), 10m);

        Assert.True(result.IsError);
        Assert.Equal(SupplierProductErrors.SupplierRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyProductId_Fails()
    {
        var result = SupplierProduct.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, 10m);

        Assert.True(result.IsError);
        Assert.Equal(SupplierProductErrors.ProductRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNegativePrice_Fails()
    {
        var result = SupplierProduct.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), -0.01m);

        Assert.True(result.IsError);
        Assert.Equal(SupplierProductErrors.InvalidPrice.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithZeroPrice_Succeeds()
    {
        var result = SupplierProduct.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 0m);

        Assert.False(result.IsError);
        Assert.Equal(0m, result.Value!.PurchasePrice);
    }

    // ---------- Update ----------

    [Fact]
    public void Update_WithValidData_Succeeds()
    {
        var sp = SupplierProduct.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10m).Value!;

        var result = sp.Update(25m, isActive: false);

        Assert.False(result.IsError);
        Assert.Equal(25m, sp.PurchasePrice);
        Assert.False(sp.IsActive);
    }

    [Fact]
    public void Update_WithNegativePrice_FailsWithoutMutating()
    {
        var sp = SupplierProduct.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 10m).Value!;

        var result = sp.Update(-5m, isActive: false);

        Assert.True(result.IsError);
        Assert.Equal(SupplierProductErrors.InvalidPrice.Code, result.TopError.Code);
        Assert.Equal(10m, sp.PurchasePrice);
        Assert.True(sp.IsActive);
    }

    // Design note (not a failing test): SupplierId, ProductId, PurchasePrice and
    // IsActive all have public setters, so every Create/Update rule above can be
    // bypassed with a plain property assignment. Consider private setters.
}
