using System;
namespace Contract.Responses
{
    public class OrderDetailForListDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal? ActualQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount => UnitPrice * (ActualQuantity == null ?  Quantity : ActualQuantity.Value);

        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; init; }

    }
}


