using Domain.Adjustments;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Adjustments;

public class AdjustmentTests
{
    // =========================================================
    // DetermineAdjustmentType — the reason -> type business rule
    // =========================================================

    [Theory]
    [InlineData(AdjustmentReason.Damaged)]
    [InlineData(AdjustmentReason.Lost)]
    [InlineData(AdjustmentReason.Expired)]
    public void DetermineAdjustmentType_LossReasons_AlwaysDecrease(AdjustmentReason reason)
    {
        var result = Adjustment.DetermineAdjustmentType(reason, null);

        Assert.True(result.IsSuccess);
        Assert.Equal(AdjustmentType.Decrease, result.Value);
    }

    [Fact]
    public void DetermineAdjustmentType_ExtraFound_AlwaysIncrease()
    {
        var result = Adjustment.DetermineAdjustmentType(AdjustmentReason.ExtraFound, null);

        Assert.Equal(AdjustmentType.Increase, result.Value);
    }

    [Fact]
    public void DetermineAdjustmentType_LossReason_SilentlyOverridesProvidedType()
    {
        // Documents current behavior: if the caller says (Damaged, Increase),
        // the type is silently overridden to Decrease. That is defensible, but
        // consider returning a validation error instead so the caller learns
        // they sent contradictory input.
        var result = Adjustment.DetermineAdjustmentType(AdjustmentReason.Damaged, AdjustmentType.Increase);

        Assert.Equal(AdjustmentType.Decrease, result.Value);
    }

    [Theory]
    [InlineData(AdjustmentReason.CountDifference)]
    [InlineData(AdjustmentReason.Other)]
    public void DetermineAdjustmentType_NeutralReasonWithoutType_Fails(AdjustmentReason reason)
    {
        var result = Adjustment.DetermineAdjustmentType(reason, null);

        Assert.True(result.IsError);
    }

    [Theory]
    [InlineData(AdjustmentReason.CountDifference, AdjustmentType.Increase)]
    [InlineData(AdjustmentReason.CountDifference, AdjustmentType.Decrease)]
    [InlineData(AdjustmentReason.Other, AdjustmentType.Increase)]
    public void DetermineAdjustmentType_NeutralReasonWithType_UsesProvidedType(
        AdjustmentReason reason, AdjustmentType type)
    {
        var result = Adjustment.DetermineAdjustmentType(reason, type);

        Assert.Equal(type, result.Value);
    }

    // =========================================================
    // Create
    // =========================================================

    [Fact]
    public void Create_WithValidData_SucceedsAsDraft()
    {
        var result = Adjustment.Create(
            Guid.NewGuid(), Guid.NewGuid(), AdjustmentReason.Damaged,
            new List<AdjustmentDetail> { TestData.ValidAdjustmentDetail() });

        Assert.True(result.IsSuccess);
        Assert.Equal(AdjustmentStatus.Draft, result.Value.AdjustmentStatus);
        Assert.Equal(AdjustmentType.Decrease, result.Value.AdjustmentType);
        Assert.Null(result.Value.AprovedAt);
    }

    [Fact]
    public void Create_WithEmptyWarehouseId_Fails()
    {
        var result = Adjustment.Create(
            Guid.NewGuid(), Guid.Empty, AdjustmentReason.Damaged,
            new List<AdjustmentDetail> { TestData.ValidAdjustmentDetail() });

        Assert.Equal(AdjustmentErrors.WarehouseRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNoDetails_Fails()
    {
        var result = Adjustment.Create(
            Guid.NewGuid(), Guid.NewGuid(), AdjustmentReason.Damaged,
            new List<AdjustmentDetail>());

        Assert.Equal(AdjustmentErrors.AdjustmentDetailsRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithUndefinedReason_Fails()
    {
        var result = Adjustment.Create(
            Guid.NewGuid(), Guid.NewGuid(), (AdjustmentReason)99,
            new List<AdjustmentDetail> { TestData.ValidAdjustmentDetail() });

        Assert.Equal(AdjustmentErrors.InvalidAdjustmentReason.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithUndefinedType_Fails()
    {
        var result = Adjustment.Create(
            Guid.NewGuid(), Guid.NewGuid(), AdjustmentReason.Other,
            new List<AdjustmentDetail> { TestData.ValidAdjustmentDetail() },
            (AdjustmentType)99);

        Assert.Equal(AdjustmentErrors.InvalidAdjustmentType.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNotesOver500Chars_Fails()
    {
        var result = Adjustment.Create(
            Guid.NewGuid(), Guid.NewGuid(), AdjustmentReason.Damaged,
            new List<AdjustmentDetail> { TestData.ValidAdjustmentDetail() },
            notes: new string('n', 501));

        Assert.Equal(AdjustmentErrors.NotesTooLong.Code, result.TopError.Code);
    }

    // =========================================================
    // UpdateStatus / locking
    // =========================================================

    [Fact]
    public void UpdateStatus_DraftToApproved_SucceedsAndStampsAprovedAt()
    {
        var adjustment = TestData.ValidAdjustment();

        var result = adjustment.UpdateStatus(AdjustmentStatus.Approved);

        Assert.True(result.IsSuccess);
        Assert.Equal(AdjustmentStatus.Approved, adjustment.AdjustmentStatus);
        Assert.NotNull(adjustment.AprovedAt);
    }

    [Fact]
    public void UpdateStatus_DraftToCancelled_DoesNotStampAprovedAt()
    {
        var adjustment = TestData.ValidAdjustment();

        adjustment.UpdateStatus(AdjustmentStatus.Cancelled);

        Assert.Null(adjustment.AprovedAt);
    }

    [Fact]
    public void UpdateStatus_OnApproved_FailsWithLocked()
    {
        var adjustment = TestData.ValidAdjustment();
        adjustment.UpdateStatus(AdjustmentStatus.Approved);

        var result = adjustment.UpdateStatus(AdjustmentStatus.Cancelled);

        Assert.Equal(AdjustmentErrors.AdjusmentIsLocked.Code, result.TopError.Code);
    }

    
    [Fact]
     public void UpdateStatus_WithUndefinedStatus_ShouldFail()
    {
        var adjustment = TestData.ValidAdjustment();

        var result = adjustment.UpdateStatus((AdjustmentStatus)999); // succeeds today

        Assert.True(result.IsError);
        Assert.Equal(AdjustmentStatus.Draft, adjustment.AdjustmentStatus);
    }

  
    [Fact]
     public void Update_OnApprovedAdjustment_ShouldFail()
    {
        var adjustment = TestData.ValidAdjustment();
        adjustment.UpdateStatus(AdjustmentStatus.Approved);

        var result = adjustment.Update("rewritten after approval");  

        Assert.True(result.IsError);
        Assert.NotEqual("rewritten after approval", adjustment.Notes);
    }

    // =========================================================
    // AddAdjustmentDetail
    // =========================================================

    [Fact]
    public void AddAdjustmentDetail_OnDraft_Succeeds()
    {
        var adjustment = TestData.ValidAdjustment();

        var result = adjustment.AddAdjustmentDetail(TestData.ValidAdjustmentDetail());

        Assert.True(result.IsSuccess);
        Assert.Equal(2, adjustment.AdjustmentDetails.Count);
    }

    [Fact]
    public void AddAdjustmentDetail_OnLocked_Fails()
    {
        var adjustment = TestData.ValidAdjustment();
        adjustment.UpdateStatus(AdjustmentStatus.Approved);

        var result = adjustment.AddAdjustmentDetail(TestData.ValidAdjustmentDetail());

        Assert.Equal(AdjustmentErrors.AdjusmentIsLocked.Code, result.TopError.Code);
        Assert.Single(adjustment.AdjustmentDetails);
    }
}

public class AdjustmentDetailTests
{
    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var result = AdjustmentDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 5m);

        Assert.True(result.IsSuccess);
        Assert.Equal(5m, result.Value.Quantity);
    }

    [Fact]
    public void Create_WithEmptyProductId_Fails()
    {
        var result = AdjustmentDetail.Create(Guid.NewGuid(), Guid.Empty, 5m);
        Assert.Equal(AdjustmentDetailErrors.ProductRequired.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Create_WithNonPositiveQuantity_Fails(decimal quantity)
    {
        var result = AdjustmentDetail.Create(Guid.NewGuid(), Guid.NewGuid(), quantity);
        Assert.Equal(AdjustmentDetailErrors.QuantityInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void UpdateQuantity_WithValidValue_Changes()
    {
        var detail = AdjustmentDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 5m).Value;

        var result = detail.UpdateQuantity(8m);

        Assert.True(result.IsSuccess);
        Assert.Equal(8m, detail.Quantity);
    }

    [Fact]
    public void UpdateQuantity_WithNonPositive_FailsAndDoesNotMutate()
    {
        var detail = AdjustmentDetail.Create(Guid.NewGuid(), Guid.NewGuid(), 5m).Value;

        var result = detail.UpdateQuantity(0m);

        Assert.True(result.IsError);
        Assert.Equal(5m, detail.Quantity);
    }
}
