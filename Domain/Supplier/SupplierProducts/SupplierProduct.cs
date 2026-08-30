using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Suppliers.SupplierProducts
{
    public class SupplierProduct : AuditableEntity
    {
        public Guid SupplierId { get; set; }
        public Guid ProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool IsActive { get; set; }
        public Products.Product? Product { get; set; }
        public Supplier? Supplier { get; set; }
        public byte [] RowVersion { get; set; } = [];
        private SupplierProduct() { }

        private SupplierProduct(
            Guid id,
            Guid supplierId,
            Guid productId,
            decimal purchasePrice,
            bool isActive) : base(id)
        {
            SupplierId = supplierId;
            ProductId = productId;
            PurchasePrice = purchasePrice;
            IsActive = isActive;
        }

        public static Result<SupplierProduct> Create(
            Guid id,
            Guid supplierId,
            Guid productId,
            decimal purchasePrice )
        {
            if (supplierId == Guid.Empty)
                return SupplierProductErrors.SupplierRequired;

            if (productId == Guid.Empty)
                return SupplierProductErrors.ProductRequired;

            if (purchasePrice < 0)
                return SupplierProductErrors.InvalidPrice;

            var entity = new SupplierProduct(
                id,
                supplierId,
                productId,
                purchasePrice,
                true
            );

            return entity;
        }

        public Result<Updated> Update(
            decimal purchasePrice,
            bool isActive)
        {
            
            if (purchasePrice < 0)
                return SupplierProductErrors.InvalidPrice;
            
            PurchasePrice = purchasePrice;
            IsActive = isActive;

            return Result.Updated;
        }

    }
}
