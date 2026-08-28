using Domain.Invoices;
using Domain.Orders;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Orders;

public class OrderTests
{
    // =========================================================
    // Create — happy paths per order type
    // =========================================================

    [Fact]
    public void Create_SaleOrder_SucceedsWithPendingStatus()
    {
        var order = TestData.ValidSaleOrder();

        Assert.Equal(OrderStatus.Pending, order.OrderStatus);
        Assert.Equal(OrderType.Sale, order.OrderType);
        Assert.NotNull(order.CustomerId);
        Assert.Null(order.SupplierId);
    }

    [Fact]
    public void Create_PurchaseOrder_KeepsSupplierAndNullsCustomer()
    {
        var order = TestData.ValidPurchaseOrder();

        Assert.NotNull(order.SupplierId);
        Assert.Null(order.CustomerId);
    }

    [Fact]
    public void Create_SaleOrder_IgnoresSupplierIdEvenIfProvided()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale,
            supplierId: Guid.NewGuid(),          // should be discarded for Sale
            customerId: Guid.NewGuid(),
            sourceWarehouseId: Guid.NewGuid(),
            destinationWarehouseId: null,
            notes: null, discountAmount: 0m,
            orderDetails: new List<OrderDetail> { TestData.ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(1));

        Assert.True(result.IsSuccess);
        Assert.Null(result.Value.SupplierId);
    }

    [Fact]
    public void Create_TransferOrder_ForcesNullDiscount()
    {
        // Business rule: transfer orders carry no money, so discount is nulled.
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Transfer,
            supplierId: null, customerId: null,
            sourceWarehouseId: Guid.NewGuid(),
            destinationWarehouseId: Guid.NewGuid(),
            notes: null,
            discountAmount: 50m,                 // caller tries to pass one anyway
            orderDetails: new List<OrderDetail> { TestData.ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(1));

        Assert.True(result.IsSuccess);
        Assert.Null(result.Value.DiscountAmount);
    }

    // =========================================================
    // Create — validation failures
    // =========================================================

    [Fact]
    public void Create_WithPastDueDate_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.NewGuid(), null, null, 0m,
            new List<OrderDetail> { TestData.ValidOrderDetail() },
            DateTimeOffset.UtcNow.AddDays(-1));

        Assert.Equal(OrderErrors.InvalidDueDateSentItMustBeMoreThanToday.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithUndefinedOrderType_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), (OrderType)99, null, Guid.NewGuid(),
            Guid.NewGuid(), null, null, 0m,
            new List<OrderDetail> { TestData.ValidOrderDetail() },
            DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.InvalidOrderType.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_PurchaseWithoutSupplier_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Purchase,
            supplierId: null, customerId: null,
            sourceWarehouseId: Guid.NewGuid(), destinationWarehouseId: null,
            notes: null, discountAmount: 0m,
            orderDetails: new List<OrderDetail> { TestData.ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(1));

        Assert.True(result.IsError);
        Assert.Equal(OrderErrors.SupplierIsRequiredForPurchasesReturnOutOperatoins.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_PurchaseWithEmptyGuidSupplier_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Purchase,
            supplierId: Guid.Empty, customerId: null,
            sourceWarehouseId: Guid.NewGuid(), destinationWarehouseId: null,
            notes: null, discountAmount: 0m,
            orderDetails: new List<OrderDetail> { TestData.ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.SupplierRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_SaleWithoutCustomer_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale,
            supplierId: null, customerId: null,
            sourceWarehouseId: Guid.NewGuid(), destinationWarehouseId: null,
            notes: null, discountAmount: 0m,
            orderDetails: new List<OrderDetail> { TestData.ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.CustomeIsRequiredForSalesReturnInOperations.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_TransferWithoutDestination_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Transfer,
            supplierId: null, customerId: null,
            sourceWarehouseId: Guid.NewGuid(), destinationWarehouseId: null,
            notes: null, discountAmount: null,
            orderDetails: new List<OrderDetail> { TestData.ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(1));

        Assert.True(result.IsError);
        Assert.Equal(OrderErrors.DestinationWarehouseIsRequiredForTransfer.Code, result.TopError.Code);
    }

 

    [Fact]
    public void Create_WithEmptySourceWarehouse_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.Empty, null, null, 0m,
            new List<OrderDetail> { TestData.ValidOrderDetail() },
            DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.SourceWarehouseRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNoOrderDetails_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.NewGuid(), null, null, 0m,
            new List<OrderDetail>(),
            DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.OrderDetailsAreRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNegativeDiscount_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.NewGuid(), null, null, -1m,
            new List<OrderDetail> { TestData.ValidOrderDetail() },
            DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.DiscountAmountInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithDiscountGreaterThanSubtotal_Fails()
    {
        // Subtotal = 2 * 50 = 100; discount 150 => NetAmount < 0
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.NewGuid(), null, null, 150m,
            new List<OrderDetail> { TestData.ValidOrderDetail(quantity: 2m, unitPrice: 50m) },
            DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.DiscountAmountLargerThanNet.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNotesOver500Chars_Fails()
    {
        var result = Order.Create(
            Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.NewGuid(), null, new string('n', 501), 0m,
            new List<OrderDetail> { TestData.ValidOrderDetail() },
            DateTimeOffset.UtcNow.AddDays(1));

        Assert.Equal(OrderErrors.NotesTooLong.Code, result.TopError.Code);
    }

    // =========================================================
    // Amount calculations
    // =========================================================

    [Fact]
    public void SubTotalAmount_SumsAllDetailTotals()
    {
        var order = Order.Create(
            Guid.NewGuid(), OrderType.Sale, null, Guid.NewGuid(),
            Guid.NewGuid(), null, null, 0m,
            new List<OrderDetail>
            {
                TestData.ValidOrderDetail(quantity: 2m, unitPrice: 50m),  // 100
                TestData.ValidOrderDetail(quantity: 3m, unitPrice: 10m),  // 30
            },
            DateTimeOffset.UtcNow.AddDays(1)).Value;

        Assert.Equal(130m, order.SubTotalAmount);
    }

    [Fact]
    public void NetAmount_WithDiscount_IsSubtotalMinusDiscount()
    {
        var order = TestData.ValidSaleOrder(discountAmount: 20m, quantity: 2m, unitPrice: 50m);

        Assert.Equal(80m, order.NetAmount); // 100 - 20
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    //
    // `public decimal NetAmount => SubTotalAmount - DiscountAmount ?? 0;`
    //
    // C# operator precedence parses this as `(SubTotal - Discount) ?? 0`,
    // NOT `SubTotal - (Discount ?? 0)`. When DiscountAmount is null (which is
    // ALWAYS true for Transfer orders), the subtraction yields null and the
    // whole expression collapses to 0 — the order's net value vanishes.
    // Fix: `SubTotalAmount - (DiscountAmount ?? 0)`.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void NetAmount_WhenDiscountIsNull_ShouldEqualSubtotal()
    {
        var order = TestData.ValidTransferOrder(); // DiscountAmount == null
        // Subtotal = 2 * 50 = 100

        Assert.Equal(100m, order.NetAmount); // FAILS: returns 0 today
    }

    // =========================================================
    // Status transitions
    // =========================================================

    [Fact]
    public void UpdateStatus_PendingToCompleted_Succeeds()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.UpdateStatus(OrderStatus.Completed);

        Assert.True(result.IsSuccess);
        Assert.Equal(OrderStatus.Completed, order.OrderStatus);
    }

    [Fact]
    public void UpdateStatus_PendingToCancelled_Succeeds()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.UpdateStatus(OrderStatus.Cancelled);

        Assert.True(result.IsSuccess);
        Assert.Equal(OrderStatus.Cancelled, order.OrderStatus);
    }

    [Fact]
    public void UpdateStatus_PendingToPending_Fails()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.UpdateStatus(OrderStatus.Pending);

        Assert.True(result.IsError);
    }

    [Fact]
    public async Task UpdateStatus_CompletingAfterDueDateHasPassed_Succeeds()
    {
        var order = TestData.ValidSaleOrder(dueDate: DateTimeOffset.UtcNow.AddMilliseconds(50));
        await Task.Delay(150);

        Assert.True(order.DueDate < DateTimeOffset.UtcNow);

        var result = order.UpdateStatus(OrderStatus.Completed);

        Assert.True(result.IsSuccess);
        Assert.Equal(OrderStatus.Completed, order.OrderStatus);
    }

    [Fact]
    public void UpdateStatus_OnCompletedOrder_FailsWithLocked()
    {
        var order = TestData.ValidSaleOrder();
        order.UpdateStatus(OrderStatus.Completed);

        var result = order.UpdateStatus(OrderStatus.Cancelled);

        Assert.Equal(OrderErrors.OrderIsLocked.Code, result.TopError.Code);
    }

    [Fact]
    public void UpdateStatus_OnCancelledOrder_FailsWithLocked()
    {
        var order = TestData.ValidSaleOrder();
        order.UpdateStatus(OrderStatus.Cancelled);

        var result = order.UpdateStatus(OrderStatus.Completed);

        Assert.Equal(OrderErrors.OrderIsLocked.Code, result.TopError.Code);
    }

    [Fact]
    public void IsLocked_TrueOnlyForCompletedOrCancelled()
    {
        var pending = TestData.ValidSaleOrder();
        Assert.False(pending.IsLocked);

        pending.UpdateStatus(OrderStatus.Completed);
        Assert.True(pending.IsLocked);
    }

    [Fact]
    public void UpdateStatus_ShouldNotOverwriteDueDate()
    {
        var dueDate = DateTimeOffset.UtcNow.AddDays(7);
        var order = TestData.ValidSaleOrder(dueDate: dueDate);

        order.UpdateStatus(OrderStatus.Cancelled);

        Assert.Equal(dueDate, order.DueDate);
    }


    // =========================================================
    // Update
    // =========================================================

    [Fact]
    public void Update_OnLockedOrder_Fails()
    {
        var order = TestData.ValidSaleOrder();
        order.UpdateStatus(OrderStatus.Completed);

        var result = order.Update(10m, "notes", null);

        Assert.Equal(OrderErrors.OrderIsLocked.Code, result.TopError.Code);
    }

    [Fact]
    public void Update_WithNegativeDiscount_Fails()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.Update(-5m, null, null);

        Assert.Equal(OrderErrors.DiscountAmountInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Update_WithNullDueDate_KeepsExistingDueDate()
    {
        var dueDate = DateTimeOffset.UtcNow.AddDays(7);
        var order = TestData.ValidSaleOrder(dueDate: dueDate);

        var result = order.Update(0m, "note", null);

        Assert.True(result.IsSuccess);
        Assert.Equal(dueDate, order.DueDate);
    }

    [Fact]
    public void Update_WithDiscountExceedingSubtotal_ShouldNotMutate()
    {
        var order = TestData.ValidSaleOrder(discountAmount: 0m, quantity: 2m, unitPrice: 50m);
        // Subtotal = 100

        var result = order.Update(discountAmount: 500m, notes: null, dueDate: null);

        Assert.True(result.IsError);
        Assert.Equal(0m, order.DiscountAmount);
    }

    // Create() enforces "Transfer orders have no discount" (forces null).
    // Update() should keep that rule too instead of applying an incoming
    // discount to any order type.
    [Fact]
    public void Update_OnTransferOrder_DiscountIsAlwaysNull()
    {
        var order = TestData.ValidTransferOrder();

        var result = order.Update(discountAmount: 25m, notes: null, dueDate: null);

        Assert.True(result.IsSuccess);
        Assert.Null(order.DiscountAmount);
    }

    [Fact]
    public void Update_OnTransferOrder_StillUpdatesNotesAndDueDate()
    {
        var order = TestData.ValidTransferOrder();
        var newDate = DateTimeOffset.UtcNow.AddDays(3);

        var result = order.Update(discountAmount: 0m, notes: "updated notes", dueDate: newDate);

        Assert.True(result.IsSuccess);
        Assert.Equal("updated notes", order.Notes);
        Assert.Equal(newDate, order.DueDate);
        Assert.Null(order.DiscountAmount);
    }

    // =========================================================
    // UpdateDuedate
    // =========================================================

    [Fact]
    public void UpdateDuedate_WithFutureDate_Succeeds()
    {
        var order = TestData.ValidSaleOrder();
        var newDate = DateTimeOffset.UtcNow.AddDays(14);

        var result = order.UpdateDuedate(newDate);

        Assert.True(result.IsSuccess);
        Assert.Equal(newDate, order.DueDate);
    }

    [Fact]
    public void UpdateDuedate_WithPastDate_Fails()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.UpdateDuedate(DateTimeOffset.UtcNow.AddDays(-1));

        Assert.Equal(OrderErrors.InvalidDueDateSentItMustBeMoreThanToday.Code, result.TopError.Code);
    }

    [Fact]
    public void UpdateDuedate_OnLockedOrder_Fails()
    {
        var order = TestData.ValidSaleOrder();
        order.UpdateStatus(OrderStatus.Cancelled);

        var result = order.UpdateDuedate(DateTimeOffset.UtcNow.AddDays(3));

        Assert.Equal(OrderErrors.OrderIsLocked.Code, result.TopError.Code);
    }

    // =========================================================
    // Order details add/remove
    // =========================================================

    [Fact]
    public void AddOrderDetail_OnPendingOrder_Succeeds()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.AddOrderDetail(TestData.ValidOrderDetail());

        Assert.True(result.IsSuccess);
        Assert.Equal(2, order.OrderDetails.Count);
    }

    [Fact]
    public void AddOrderDetail_OnLockedOrder_Fails()
    {
        var order = TestData.ValidSaleOrder();
        order.UpdateStatus(OrderStatus.Completed);

        var result = order.AddOrderDetail(TestData.ValidOrderDetail());

        Assert.Equal(OrderErrors.OrderIsLocked.Code, result.TopError.Code);
        Assert.Single(order.OrderDetails);
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    //
    // Create() rejects an order with zero details, but RemoveOrderDetail lets
    // you strip the LAST detail from a pending order, producing exactly the
    // state Create() forbids. Guard: refuse removal when it would leave the
    // order empty.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void RemoveOrderDetail_LastDetail_ShouldFail()
    {
        var order = TestData.ValidSaleOrder();
        var onlyDetail = order.OrderDetails.First();

        var result = order.RemoveOrderDetail(onlyDetail);

        Assert.True(result.IsError);        // FAILS: succeeds today
        Assert.NotEmpty(order.OrderDetails); // FAILS: collection is empty
    }
     
    [Fact]
    [Trait("Category", "BugExposing")]
    public void AddOrderDetail_WithNull_ShouldFail()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.AddOrderDetail(null!);

        Assert.True(result.IsError);
        // Accessing SubTotalAmount after adding null throws NullReferenceException today:
        _ = order.SubTotalAmount;
    }

    // =========================================================
    // Invoices
    // =========================================================

    private static Invoice MakeInvoice(Guid orderId)
    {
        var line = InvoiceLineItem.Create(
            lineNo: 1, invoiceId: Guid.NewGuid(), name: "Item",
            tax: 5m, quantity: 2m, unitPrice: 50m).Value;

        return Invoice.Create(Guid.NewGuid(), InvoiceType.Sale, 0m,
            new List<InvoiceLineItem> { line }, orderId).Value;
    }

    [Fact]
    public void IssueInvoice_OnPendingOrder_Fails()
    {
        var order = TestData.ValidSaleOrder();

        var result = order.IssueInvoice(MakeInvoice(order.Id));

        Assert.Equal(OrderErrors.CannotIssueInvoiceBeforeOrderCompeletion.Code, result.TopError.Code);
    }

    [Fact]
    public void IssueInvoice_OnCompletedSaleOrder_Succeeds()
    {
        var order = TestData.ValidSaleOrder();
        order.UpdateStatus(OrderStatus.Completed);

        var result = order.IssueInvoice(MakeInvoice(order.Id));

        Assert.True(result.IsSuccess);
        Assert.NotNull(order.Invoice);
    }

    [Fact]
    public void IssueInvoice_OnCompletedTransferOrder_Fails()
    {
        var order = TestData.ValidTransferOrder();
        order.UpdateStatus(OrderStatus.Completed);

        var result = order.IssueInvoice(MakeInvoice(order.Id));

        Assert.True(result.IsError); // transfers are not invoiceable
    }

   
    [Fact]
     public void IssueInvoice_Twice_ShouldFailSecondTime()
    {
        var order = TestData.ValidSaleOrder();
        order.UpdateStatus(OrderStatus.Completed);

        var first = MakeInvoice(order.Id);
        order.IssueInvoice(first);

        var second = MakeInvoice(order.Id);
        var result = order.IssueInvoice(second); 
        Assert.True(result.IsError);
        Assert.Same(first, order.Invoice);
    }

    // =========================================================
    // GetAssciatedInvoiceType
    // =========================================================

    [Theory]
    [InlineData(OrderType.Purchase, InvoiceType.Purchase)]
    [InlineData(OrderType.Sale, InvoiceType.Sale)]
    [InlineData(OrderType.ReturnIn, InvoiceType.ReturnIn)]
    [InlineData(OrderType.ReturnOut, InvoiceType.ReturnOut)]
    public void GetAssciatedInvoiceType_MapsCorrectly(OrderType orderType, InvoiceType expected)
    {
        var order = orderType switch
        {
            OrderType.Purchase or OrderType.ReturnOut => Order.Create(
                Guid.NewGuid(), orderType, Guid.NewGuid(), null, Guid.NewGuid(), null,
                null, 0m, new List<OrderDetail> { TestData.ValidOrderDetail() },
                DateTimeOffset.UtcNow.AddDays(1)).Value,
            _ => Order.Create(
                Guid.NewGuid(), orderType, null, Guid.NewGuid(), Guid.NewGuid(), null,
                null, 0m, new List<OrderDetail> { TestData.ValidOrderDetail() },
                DateTimeOffset.UtcNow.AddDays(1)).Value,
        };

        var result = order.GetAssciatedInvoiceType();

        Assert.True(result.IsSuccess);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void GetAssciatedInvoiceType_ForTransfer_Fails()
    {
        var order = TestData.ValidTransferOrder();

        var result = order.GetAssciatedInvoiceType();

        Assert.True(result.IsError);
    }

    [Fact]
    public void CanIssueInvoiceForOrderType_FalseOnlyForTransfer()
    {
        Assert.True(Order.CanIssueInvoiceForOrderType(OrderType.Purchase));
        Assert.True(Order.CanIssueInvoiceForOrderType(OrderType.Sale));
        Assert.True(Order.CanIssueInvoiceForOrderType(OrderType.ReturnIn));
        Assert.True(Order.CanIssueInvoiceForOrderType(OrderType.ReturnOut));
        Assert.False(Order.CanIssueInvoiceForOrderType(OrderType.Transfer));
    }
}
