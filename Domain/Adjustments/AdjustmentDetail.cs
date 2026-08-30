using Domain.Products;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;

namespace Domain.Adjustments
{
    public class AdjustmentDetail : AuditableEntity
    {
        public Guid AdjustmentId { get; private set; }
        public Adjustment Adjustment { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }
        public byte[] RowVersion { get; private set; }
        private AdjustmentDetail() { }

        private AdjustmentDetail(
            Guid id,
            Guid productId,
            decimal quantity) : base(id)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public static Result<AdjustmentDetail> Create(
            Guid id,
            Guid productId,
            
            decimal quantity)
        {
        
            if (productId == Guid.Empty)
                return AdjustmentDetailErrors.ProductRequired;

            if (quantity <= 0)
                return AdjustmentDetailErrors.QuantityInvalid;

            var detail = new AdjustmentDetail(
                id,
                productId,
                quantity);

            return detail;
        }

        public Result<Updated> UpdateQuantity(decimal quantity) {

            if (quantity <= 0)
                return AdjustmentDetailErrors.QuantityInvalid;
        
            this.Quantity = quantity;
            return Result.Updated;
        }

    }
}

