using Domain.Warehouses;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Contract.Features.Inventory.WarehouseStock.DTOs;
using Contract.Features.Inventory.Product.Mappers;

namespace Contract.Features.Inventory.WarehouseStocks.Mappers
{
    public static class WarehouseStockMapper
    {
        public static WarehouseStockDtoForList ToDtoForList(this Domain.Warehouses.WarehouseStock entity)
        {
            return new WarehouseStockDtoForList
            {
                Id = entity.Id,
                Quantity = entity.Quantity,
                RowVersion = entity.RowVersion,
                MinimumStockLevel = entity.MinimumStockLevel,
                ProductId = entity.Product!.Id,
                SKU = entity.Product!.SKU,
                BarCode = entity.Product!.BarCode,
                ProductName = entity.Product!.ProductName,
                SellingPrice = entity.Product!.SellingPrice,
                IsActive = entity.Product!.IsActive,
                Unit = entity.Product!.Unit.ToString(),
                Category = entity.Product!.Category!.Name ,
                CreatedBy = entity.CreatedBy ,
                LastModifiedBy = entity.LastModifiedBy 

            };
        }
        public static WarehouseStockDto ToDto(this Domain.Warehouses.WarehouseStock entity)
        {
            return new WarehouseStockDto
            {
                Id = entity.Id,
                WarehouseId = entity.WarehouseId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                MinimumStockLevel = entity.MinimumStockLevel,
                RowVersion = entity.RowVersion,

            };
        }
    }
}

