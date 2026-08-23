using Domain.Orders;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Orders;

public class OrderDetailTests
{
    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var result = OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 5m, 10m);

        Assert.True(result.IsSuccess);
        Assert.Equal(5m, result.Value.Quantity);
        Assert.Equal(10m, result.Value.UnitPrice);
        Assert.Null(result.Value.ActualQuantity);
    }

    [Fact]
    public void Create_WithEmptyProductId_Fails()
    {
        var result = OrderDetail.Create(Guid.NewGuid(), Guid.Empty, 5m, 10m);
        Assert.Equal(OrderDetailErrors.ProductRequired.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WithNonPositiveQuantity_Fails(decimal quantity)
    {
        var result = OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), quantity, 10m);
        Assert.Equal(OrderDetailErrors.QuantityInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNegativeUnitPrice_Fails()
    {
        var result = OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 5m, -0.01m);
        Assert.Equal(OrderDetailErrors.UnitPriceInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithZeroUnitPrice_Succeeds()
    {
        // Documents current rule: free line items are allowed (e.g. samples).
        var result = OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 5m, 0m);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void TotalAmount_UsesQuantityWhenActualQuantityIsNull()
    {
        var detail = OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 4m, 25m).Value;

        Assert.Equal(100m, detail.TotalAmount);
    }

    [Fact]
    public void UpdateQuantity_WithValidValue_Changes()
    {
        var detail = OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 4m, 25m).Value;

        var result = detail.UpdateQuantity(10m);

        Assert.True(result.IsSuccess);
        Assert.Equal(10m, detail.Quantity);
        Assert.Equal(250m, detail.TotalAmount);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void UpdateQuantity_WithNonPositive_FailsAndDoesNotMutate(decimal quantity)
    {
        var detail = OrderDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 4m, 25m).Value;

        var result = detail.UpdateQuantity(quantity);

        Assert.True(result.IsError);
        Assert.Equal(4m, detail.Quantity);
    }

    // DESIGN NOTE (documented, not asserted):
    //
    // 1. ActualQuantity participates in TotalAmount (`ActualQuantity ?? Quantity`)
    //    and has an unused error `ActualQuantityInvalid`, but there is NO method
    //    that can ever set it — Create always passes null. Either add
    //    `UpdateActualQuantity(decimal)` (with the >= 0 validation) or delete the
    //    property; right now it is dead weight that suggests a half-finished
    //    "ordered vs received" feature.
    //
    // 2. UpdateQuantity does not consult the parent Order's IsLocked flag, so a
    //    completed order's line quantities (and therefore its amounts) can still
    //    be changed if application code holds a reference to the detail. The
    //    lock check lives only on Order.Add/RemoveOrderDetail. Consider routing
    //    all detail mutations through the Order aggregate root.
}
