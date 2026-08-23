using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Transactions.Order.DTOs
{
    public sealed record OrderForListDto
    {
        public Guid Id { get; init; }
        public string OrderType { get; init; } = string.Empty;
        public string OrderStatus { get; init; } = string.Empty;

        public Guid? SupplierId { get; init; }
        public string? SupplierName { get; init; }

        public Guid? CustomerId { get; init; }
        public string? CustomerName { get; init; }

        public Guid? InvoiceId { get; init; }

        public Guid? SourceWarehouseId { get; init; }
        public string? SourceWarehouseName { get; init; }

        public Guid? DestinationWarehouseId { get; init; }
        public string? DestinationWarehouseName { get; init; }

        public DateTimeOffset DueDate { get; init; }

        public decimal? NetAmount { get; init; }
        public decimal? SubTotalAmount { get; init; }
        public decimal? DiscountAmount { get; init; }

        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; init; }

    }
}

