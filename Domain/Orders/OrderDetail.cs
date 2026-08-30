using Domain.Products;
using Domain.Warehouses;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;

namespace Domain.Orders
{
    public class OrderDetail : AuditableEntity
    {
        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }
      
        public Guid ProductId { get; private set; }

        public Product? Product { get; private set; }

        public decimal Quantity { get; private set; }
        public decimal? ActualQuantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalAmount => UnitPrice * (ActualQuantity ?? Quantity) ;

        public byte[] RowVersion { get; private set; }

        private OrderDetail() { }

        private OrderDetail(
            Guid id,
             Guid productId,
            decimal quantity,
            decimal? actualQuantity,
            decimal unitPrice) : base(id)
        {
             ProductId = productId;
            Quantity = quantity;
            ActualQuantity = actualQuantity;
            UnitPrice = unitPrice;
        }

        public static Result<OrderDetail> Create(
            Guid id,
             Guid productId,
            decimal quantity,
            decimal unitPrice)
        {
            
            if (productId == Guid.Empty)
                return OrderDetailErrors.ProductRequired;

            if (quantity <= 0)
                return OrderDetailErrors.QuantityInvalid;

           
            if (unitPrice < 0)
                return OrderDetailErrors.UnitPriceInvalid;

            var orderDetail = new OrderDetail(
                id,
                 productId,
                quantity,
                null,
                unitPrice);

            return orderDetail;
        }
        public Result<Updated> UpdateQuantity(
       
            decimal quantity)
        {
            
            
            if (quantity <= 0)
                return OrderDetailErrors.QuantityInvalid;
            
            Quantity = quantity;

            return Result.Updated;
        }



    }
}

