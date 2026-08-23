using Domain.Suppliers.SupplierProducts;
using Contract.Features.Parties.SupplierProducts.DTOs;
using Contract.Features.Parties.SupplierProduct.DTOs;

namespace Contract.Features.Parties.SupplierProducts.Mappers
{
    public static class SupplierProductMapper
    {
        public static SupplierProductDtoForList ToDtoForList(this Domain.Suppliers.SupplierProducts.SupplierProduct entity)
        {
            return new SupplierProductDtoForList
            {
                Id = entity.Id,
                SupplierId = entity.SupplierId,
                ProductId = entity.ProductId,
                PurchasePrice = entity.PurchasePrice,
                IsActive = entity.IsActive ,
                ProductName = entity.Product?.ProductName ?? string.Empty, 
                CreatedAt = entity.CreatedAtUtc , 
                UpdatedAt = entity.LastModifiedUtc ,

             };
        }
        public static SupplierProductDto ToDto(this Domain.Suppliers.SupplierProducts.SupplierProduct entity)
        {
            return new SupplierProductDto
            {
                Id = entity.Id,
                SupplierId = entity.SupplierId,
                ProductId = entity.ProductId,
                PurchasePrice = entity.PurchasePrice,
                IsActive = entity.IsActive
            };
        }
    }
}

