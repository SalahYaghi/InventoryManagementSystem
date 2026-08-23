using Domain.Orders;
using Contract.Features.Transactions.Order.DTOs;
using Contract.Features.Inventory.Product.Mappers;

namespace Contract.Features.Transactions.Order.Mappers
{
    public static class OrderDetailMapper
    {
        public static OrderDetailDto ToDto(this Domain.Orders.OrderDetail entity)
        {
            return new OrderDetailDto
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                ActualQuantity = entity.ActualQuantity,
                UnitPrice = entity.UnitPrice,
                Product = entity.Product?.ToDto(),
                RowVersion = entity.RowVersion

            }; 

        }
    }
}

