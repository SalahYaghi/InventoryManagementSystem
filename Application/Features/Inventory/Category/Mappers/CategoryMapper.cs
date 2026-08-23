using Domain.Products.Category;
using Contract.Features.Inventory.Categories.DTOs;

namespace Contract.Features.Inventory.Categories.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Domain.Products.Category.Category entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}

