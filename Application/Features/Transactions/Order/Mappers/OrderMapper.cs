using Domain.Orders;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Inventory.Warehouses.Mappers;
using Contract.Features.Parties.Customers.Mappers;
using Contract.Features.Parties.Supplier.Mappers;
using Contract.Features.Transactions.Invoice.Mappers;
using Contract.Features.Transactions.Order.Mappers;

namespace Contract.Features.Transactions.Orders.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Domain.Orders.Order entity)
        {
            return new OrderDto
            {
                Id = entity.Id,
                OrderType = entity.OrderType,
                OrderStatus = entity.OrderStatus,
                SupplierId = entity.SupplierId,
                CustomerId = entity.CustomerId,
                InvoiceId = entity.InvoiceId,
                SourceWarehouseId = entity.SourceWarehouseId,
                DestinationWarehouseId = entity.DestinationWarehouseId,
                NetAmount = entity.NetAmount,
                SubTotalAmount = entity.SubTotalAmount,
                DiscountAmount = entity.DiscountAmount ?? 0,
                Notes = entity.Notes,
                SourceWarehouseDto = entity.SourceWarehouse?.ToDto(),
                DestinationWarehouseDto = entity.DestinationWarehouse?.ToDto(),
                Customer = entity.Customer?.ToDto(),
                OrderDetails = entity.OrderDetails.Select(e => e.ToDto()).ToList(),
                Supplier= entity.Supplier?.ToDto(),
                DueDate = entity.DueDate,
            };
        }
    }
}

