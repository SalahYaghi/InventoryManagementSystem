using MechanicShop.Domain.Common;
using System;
using MechanicShop.Domain.Common.Results;
using Domain.Products;
using System.Runtime.CompilerServices;
using Domain.Common.Results.Interfaces;


namespace Domain.Warehouses
{
    public class WarehouseStock : AuditableEntity   , ISoftDeletable
    {
        public byte[] RowVersion { get; private set; }

        public Guid WarehouseId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal MinimumStockLevel { get; private set; }

        public Product? Product { get; private set; }
        public Warehouse? Warehouse { get; private set; }
         public bool? IsDeleted { get; set; }
         public DateTimeOffset? DeletedAt { get; set; }

        private WarehouseStock() { }

        private WarehouseStock(
            Guid id,
            Guid warehouseId,
            Guid productId,
            decimal quantity,
            decimal minimumStockLevel) : base(id)
        {
            WarehouseId = warehouseId;
            ProductId = productId;
            Quantity = quantity;
            MinimumStockLevel = minimumStockLevel;
        }

        public Result<Updated> RemoveQuantity(decimal quantity)
        {

            if (quantity <= 0)
                return WarehouseStockErrors.QuantityInvalid;

            if(quantity > this.Quantity)
                return WarehouseStockErrors.QuantityExccededAlowedAmount;

            this.Quantity -= quantity;
            return Result.Updated;
        }
        public  Result<Updated> AddToQuantity(decimal quantity) {

            if (quantity <= 0)
                return WarehouseStockErrors.QuantityInvalid;

            this.Quantity += quantity;
            return Result.Updated;
        }
        public static Result<WarehouseStock> Create(
            Guid id,
            Guid warehouseId,
            Guid productId,
             decimal minimumStockLevel , 
             decimal quantity = 0)
        {
            if (warehouseId == Guid.Empty)
                return WarehouseStockErrors.WarehouseRequired;

            if (productId == Guid.Empty)
                return WarehouseStockErrors.ProductRequired;
             
            if (minimumStockLevel < 0)
                return WarehouseStockErrors.MinimumStockLevelInvalid;

            if (quantity < 0)
                return WarehouseStockErrors.QuantityInvalid;

            var warehouseStock = new WarehouseStock(
                id,
                warehouseId,
                productId,
                quantity,
                 minimumStockLevel);

            return warehouseStock;
        }
        public Result<Updated> UpdateMinimumLevel(
            decimal minimumStockLevel)
        {
            if (minimumStockLevel < 0)
                return WarehouseStockErrors.MinimumStockLevelInvalid;

            MinimumStockLevel = minimumStockLevel;

            return Result.Updated;
        }

    }
}

