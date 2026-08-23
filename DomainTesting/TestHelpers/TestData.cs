using Domain.Adjustments;
using Domain.Orders;
using Domain.Warehouses;


// The domain uses namespaces that contain a class with the same name
// (e.g. Domain.Contacts.Address.Address), so aliases are required.
using AddressEntity = Domain.Contacts.Address.Address;
using ContactInfoEntity = Domain.Contacts.ContactInfo.ContactInfo;

namespace InventoryManagement.Application.DomainTesting.TestHelpers;

/// <summary>
/// Builders that produce VALID domain objects. Every builder asserts success,
/// so if a factory's validation rules change and break these, the failure
/// surfaces immediately with a clear message.
/// </summary>
public static class TestData
{
    public static ContactInfoEntity ValidContact(
        string email = "test@example.com",
        string phone = "+972590000000")
    {
        var result = ContactInfoEntity.Create(
            Guid.NewGuid(), email, phone,
            alternitavePhoneNumber: "0590000001",
            faxNumber: null,
            websiteUrl: "https://example.com");

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidContact produced an invalid contact: {result.TopError.Code} - {result.TopError.Description}");

        return result.Value;
    }

    public static AddressEntity ValidAddress(
        Guid? countryId = null,
        Guid? cityId = null)
    {
        var result = AddressEntity.Create(
            Guid.NewGuid(),
            countryId ?? Guid.NewGuid(),
            cityId ?? Guid.NewGuid(),
            postalCode: "12345",
            buildingNumber: "10A",
            street: "Main St",
            description: "Near the market");

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidAddress produced an invalid address: {result.TopError.Code} - {result.TopError.Description}");

        return result.Value;
    }

    public static OrderDetail ValidOrderDetail(
        decimal quantity = 2m,
        decimal unitPrice = 50m,
        Guid? productId = null)
    {
        var result = OrderDetail.Create(
            Guid.NewGuid(),
            productId ?? Guid.NewGuid(),
            quantity,
            unitPrice);

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidOrderDetail failed: {result.TopError.Code}");

        return result.Value;
    }

    /// <summary>Sale order: 1 line, subtotal = quantity * unitPrice (default 100).</summary>
    public static Order ValidSaleOrder(
        decimal? discountAmount = 0m,
        decimal quantity = 2m,
        decimal unitPrice = 50m,
        DateTimeOffset? dueDate = null)
    {
        var result = Order.Create(
            Guid.NewGuid(),
            OrderType.Sale,
            supplierId: null,
            customerId: Guid.NewGuid(),
            sourceWarehouseId: Guid.NewGuid(),
            destinationWarehouseId: null,
            notes: null,
            discountAmount: discountAmount,
            orderDetails: new List<OrderDetail> { ValidOrderDetail(quantity, unitPrice) },
            dueDate: dueDate ?? DateTimeOffset.UtcNow.AddDays(7));

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidSaleOrder failed: {result.TopError.Code}");

        return result.Value;
    }

    public static Order ValidPurchaseOrder()
    {
        var result = Order.Create(
            Guid.NewGuid(),
            OrderType.Purchase,
            supplierId: Guid.NewGuid(),
            customerId: null,
            sourceWarehouseId: Guid.NewGuid(),
            destinationWarehouseId: null,
            notes: null,
            discountAmount: 0m,
            orderDetails: new List<OrderDetail> { ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(7));

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidPurchaseOrder failed: {result.TopError.Code}");

        return result.Value;
    }

    public static Order ValidTransferOrder()
    {
        var result = Order.Create(
            Guid.NewGuid(),
            OrderType.Transfer,
            supplierId: null,
            customerId: null,
            sourceWarehouseId: Guid.NewGuid(),
            destinationWarehouseId: Guid.NewGuid(),
            notes: null,
            discountAmount: null,
            orderDetails: new List<OrderDetail> { ValidOrderDetail() },
            dueDate: DateTimeOffset.UtcNow.AddDays(7));

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidTransferOrder failed: {result.TopError.Code}");

        return result.Value;
    }

    public static AdjustmentDetail ValidAdjustmentDetail(decimal quantity = 5m)
    {
        var result = AdjustmentDetail.Create(Guid.NewGuid(), Guid.NewGuid(), quantity);

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidAdjustmentDetail failed: {result.TopError.Code}");

        return result.Value;
    }

    public static Adjustment ValidAdjustment(
        AdjustmentReason reason = AdjustmentReason.Damaged,
        AdjustmentType? type = null)
    {
        var result = Adjustment.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            reason,
            new List<AdjustmentDetail> { ValidAdjustmentDetail() },
            type);

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidAdjustment failed: {result.TopError.Code}");

        return result.Value;
    }

    public static WarehouseStock ValidWarehouseStock(
        decimal minimumStockLevel = 10m,
        decimal quantity = 100m)
    {
        var result = WarehouseStock.Create(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            minimumStockLevel, quantity);

        if (result.IsError)
            throw new InvalidOperationException(
                $"TestData.ValidWarehouseStock failed: {result.TopError.Code}");

        return result.Value;
    }
}
