using Contract.Features.Transactions.Order.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Domain.Orders;

namespace InventoryManagement.Tests.Common.Factories.Orders;

public static class OrderDetailFactory
{
    public static OrderDetail CreateValid(
        Guid? id = null,
        Guid? productId = null,
        decimal quantity = 2m,
        decimal unitPrice = 10m)
    {
        var result = OrderDetail.Create(
            id ?? Guid.NewGuid(),
            productId ?? Guid.NewGuid(),
            quantity,
            unitPrice);

        if (result.IsError)
            throw new InvalidOperationException(result.TopError.Description);

        return result.Value;
    }
    public static CreateOrderDetailCommand CreateValidOrderDetailsCommand()
    {

        return new CreateOrderDetailCommand()
        {
            ProductId = Guid.Parse("4D4ED1E0-A406-42FB-B3A1-00018841AEBD"),
            Quantity = 2m, 
            RowVersion = new byte[] { 0 } ,
            OrderId   = Guid.Parse("4D4ED1E0-A406-42FB-B3A1-00018841AEBD")

        };
    }
    public static Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail.CreateOrderDetailCommand CreateValidOrderDetailsCommandForOrderGeneral()
    {

        return new Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail.CreateOrderDetailCommand()
        {
            ProductId = Guid.Parse("4D4ED1E0-A406-42FB-B3A1-00018841AEBD"),
            Quantity = 2m,
            RowVersion = new byte[] { 0 } , 
           


        };
    }
}
