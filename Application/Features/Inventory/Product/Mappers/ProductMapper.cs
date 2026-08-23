using Domain;
using Contract.Features.Inventory.Product.DTOs;
using Domain.Products.Domain.Products;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Categories.Mappers;

namespace Contract.Features.Inventory.Product.Mappers
{
    public static class ProductMapper
    {

        public static ProductDto ToDto(this Domain.Products.Product entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                SKU = entity.SKU,
                BarCode = entity.BarCode,
                ProductName = entity.ProductName,
                Description = entity.Description,
                SellingPrice = entity.SellingPrice,
                IsActive = entity.IsActive,
                Unit = entity.Unit,
                CategoryId = entity.CategoryId,
                Category = entity.Category?.ToDto()
            };

        }
        public static ProductDtoForList ToListDto(this Domain.Products.Product entity)
        {
            return new ProductDtoForList
            {
                Id = entity.Id,
                SKU = entity.SKU,
                BarCode = entity.BarCode,
                ProductName = entity.ProductName,
                SellingPrice = entity.SellingPrice,
                IsActive = entity.IsActive,
                Unit = entity.Unit.ToString(),
                Category = entity.Category?.Name ,
            };
        }
    }
}

