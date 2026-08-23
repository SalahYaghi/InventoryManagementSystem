using Domain.Adjustments;
using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Inventory.Product.Mappers;

namespace Contract.Features.Inventory.Adjustment.Mappers
{
    public static class AdjustmentDetailMapper
    {
        public static AdjustmentDetailDto ToDto(this Domain.Adjustments.AdjustmentDetail entity)
        {
            return new AdjustmentDetailDto
            {
                Id = entity.Id,
                AdjustmentId = entity.AdjustmentId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity , 
                RowVersion = entity.RowVersion,
                Product = entity.Product?.ToDto()
                
            };
        }
    }
}

