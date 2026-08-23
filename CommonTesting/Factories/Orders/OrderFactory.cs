using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Domain.Orders;

namespace InventoryManagement.Tests.Common.Factories.Orders;

public static class OrderFactory
{
    public static Order CreatePurchase(Guid? id = null, Guid? supplierId = null, Guid? sourceWarehouseId = null, DateTimeOffset? dueDate = null, string? notes = "Valid purchase order", decimal? discountAmount = 0m, List<OrderDetail>? orderDetails = null)
    {
        var result = Order.Create(id ?? Guid.NewGuid(), OrderType.Purchase, supplierId ?? Guid.NewGuid(), null, sourceWarehouseId ?? Guid.NewGuid(), null, notes, discountAmount, orderDetails ?? new List<OrderDetail> { OrderDetailFactory.CreateValid() }, dueDate ?? DateTimeOffset.UtcNow.AddDays(1));
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }

    public static Order CreateSale(Guid? id = null, Guid? customerId = null, Guid? sourceWarehouseId = null, DateTimeOffset? dueDate = null, string? notes = "Valid sale order", decimal? discountAmount = 0m, List<OrderDetail>? orderDetails = null)
    {
        var result = Order.Create(id ?? Guid.NewGuid(), OrderType.Sale, null, customerId ?? Guid.NewGuid(), sourceWarehouseId ?? Guid.NewGuid(), null, notes, discountAmount, orderDetails ?? new List<OrderDetail> { OrderDetailFactory.CreateValid() }, dueDate ?? DateTimeOffset.UtcNow.AddDays(1));
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }

    public static Order CreateTransfer(Guid? id = null, Guid? sourceWarehouseId = null, Guid? destinationWarehouseId = null, DateTimeOffset? dueDate = null, string? notes = "Valid transfer order", List<OrderDetail>? orderDetails = null)
    {
        var result = Order.Create(id ?? Guid.NewGuid(), OrderType.Transfer, null, null, sourceWarehouseId ?? Guid.NewGuid(), destinationWarehouseId ?? Guid.NewGuid(), notes, null, orderDetails ?? new List<OrderDetail> { OrderDetailFactory.CreateValid() }, dueDate ?? DateTimeOffset.UtcNow.AddDays(1));
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }


    public static CreateOrderCommand CreateValidPurchaseOrderCommand() {

        return new CreateOrderCommand()
        {

            SupplierId = Guid.Parse(@"61CCAF8E-7C64-4315-B7D6-0917CBAF0928"),
            Discount = 10m,
            Notes = "Valid order command",
            SourceWarehouseId = Guid.Parse("C09C1A4F-0199-476D-9B5D-062765DCD374"),
            OrderType = OrderType.Purchase,
            DueDate = DateTimeOffset.UtcNow.AddDays(1),
            OrderDetails = new List<CreateOrderDetailCommand> { OrderDetailFactory.CreateValidOrderDetailsCommandForOrderGeneral() } , 

        };
    }



}
