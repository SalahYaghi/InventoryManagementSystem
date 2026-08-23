using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Transactions.Order.DTOs
{
    public sealed record OrderDetailForListDto
    {
        public Guid Id { get; init; }
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public decimal Quantity { get; init; }
        public decimal? ActualQuantity { get; init; }
        public byte[] RowVersion { get; init; } = [];
        public decimal UnitPrice { get; init; }
        public decimal TotalAmount => UnitPrice * (ActualQuantity ?? Quantity);
    }
}

