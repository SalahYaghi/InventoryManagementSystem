using Domain.Adjustments;
using Contract.Features.Inventory.Adjustments.DTOs;
using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Inventory.Product.Mappers;
using Contract.Features.Inventory.Warehouses.Mappers;

namespace Contract.Features.Inventory.Adjustments.Mappers
{
    public static class AdjustmentMapper
    {
        public static AdjustmentDto ToDto(this Domain.Adjustments.Adjustment entity)
        {
            return new AdjustmentDto
            {
                Id = entity.Id,
                WarehouseId = entity.WarehouseId,
                AdjustmentType = entity.AdjustmentType,
                AdjustmentReason = entity.AdjustmentReason,
                AdjustmentStatus = entity.AdjustmentStatus,
                Notes = entity.Notes,
                AdjustmentDetailDtos = entity.AdjustmentDetails.Select(a => new AdjustmentDetailDto() {
                            
                    Id = a.Id,
                    AdjustmentId = a.AdjustmentId,
                    ProductId = a.ProductId,
                    Quantity = a.Quantity ,
                    Product = a?.Product?.ToDto(),
                    RowVersion = a?.RowVersion == null ? new byte[8] : a.RowVersion
                   
                }).ToList(), 
                Warehouse = entity.Warehouse?.ToDto(),
                
            };
        }
    }
}

