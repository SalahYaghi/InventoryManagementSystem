using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.Transactions.Invoice.DTOs;
using Contract.Features.Transactions.Order.DTOs;
using Domain.Orders;

namespace Contract.Features.Transactions.Orders.DTOs
{

    

    public sealed record OrderDto
    {
        public Guid Id { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; } = [];

        public Guid? SupplierId { get; set; }
        public SupplierDto? Supplier { get; set; }

        public Guid? CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }

        public Guid? InvoiceId { get; set; }
        public InvoiceDto? Invoice { get; set; }

        public Guid? SourceWarehouseId { get; set; }
        public WarehouseDto? SourceWarehouseDto { get; set; }

        public DateTimeOffset DueDate { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public WarehouseDto? DestinationWarehouseDto { get; set; }

        public decimal NetAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string? Notes { get; set; }
    }
}

