using Domain.Common.Results.Interfaces;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using MechanicShop.Domain.Common;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Common;

public class EntityTests
{
    private sealed class TestEntity : Entity
    {
        public TestEntity(Guid id) : base(id) { }
    }

    private sealed class TestEvent : DomainEvent { }

    [Fact]
    public void Constructor_WithEmptyGuid_GeneratesNewId()
    {
        var entity = new TestEntity(Guid.Empty);

        Assert.NotEqual(Guid.Empty, entity.Id);
    }

    [Fact]
    public void Constructor_WithProvidedGuid_KeepsIt()
    {
        var id = Guid.NewGuid();
        var entity = new TestEntity(id);

        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void DomainEvents_AddRemoveClear_WorkAsExpected()
    {
        var entity = new TestEntity(Guid.NewGuid());
        var ev = new TestEvent();

        entity.AddDomainEvent(ev);
        Assert.Single(entity.DomainEvents);

        entity.RemoveDomainEvent(ev);
        Assert.Empty(entity.DomainEvents);

        entity.AddDomainEvent(new TestEvent());
        entity.AddDomainEvent(new TestEvent());
        entity.ClearDomainEvents();
        Assert.Empty(entity.DomainEvents);
    }
}

public class SoftDeleteTests
{
    // WarehouseStock is the domain's ISoftDeletable implementation,
    // so it is used to exercise the interface's default methods.

    [Fact]
    public void Delete_FirstTime_SetsIsDeletedAndTimestamp()
    {
        ISoftDeletable stock = TestData.ValidWarehouseStock();

        stock.Delete();

        Assert.True(stock.IsDeleted);
        Assert.NotNull(stock.DeletedAt);
    }

    [Fact]
    public void UndoDelete_ResetsFlagsToNotDeleted()
    {
        ISoftDeletable stock = TestData.ValidWarehouseStock();
        stock.Delete();

        stock.UndoDelete();

        Assert.False(stock.IsDeleted);
        Assert.Null(stock.DeletedAt);
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    //
    // ISoftDeletable.Delete() guards with `if (IsDeleted.HasValue) return;`.
    // After UndoDelete(), IsDeleted == false — which still HasValue — so every
    // later Delete() call is silently ignored. The entity can never be deleted
    // again. The guard should be `if (IsDeleted == true) return;`.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Delete_AfterUndoDelete_ShouldDeleteAgain()
    {
        ISoftDeletable stock = TestData.ValidWarehouseStock();

        stock.Delete();
        stock.UndoDelete();
        stock.Delete(); // silently no-ops today

        Assert.True(stock.IsDeleted);      // FAILS: IsDeleted is false
        Assert.NotNull(stock.DeletedAt);   // FAILS: DeletedAt is null
    }

    [Fact]
    public void Delete_CalledTwice_IsIdempotent()
    {
        ISoftDeletable stock = TestData.ValidWarehouseStock();

        stock.Delete();
        var firstDeletedAt = stock.DeletedAt;
        stock.Delete();

        Assert.True(stock.IsDeleted);
        Assert.Equal(firstDeletedAt, stock.DeletedAt);
    }
}
