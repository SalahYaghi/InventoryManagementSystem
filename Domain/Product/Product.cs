using Domain.Common.Helpers;
using Domain.Products.Category;
using Domain.Products.Domain.Products;
using Domain.Products.Enums;
using Domain.Warehouses;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Products
{

    public class Product : AuditableEntity
    {
        public string SKU { get; private set; } = string.Empty;
        public string? BarCode { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal SellingPrice { get; private set; }
        public bool IsActive { get; private set; }
        public Unit Unit { get; private set; }
        public Category.Category ?Category { get; private set; }
        public Guid CategoryId { get; private set; }
    
        private readonly List<ProductImage> _productImages = new();
        public IReadOnlyCollection<ProductImage> ProductImages => _productImages;


        private readonly List<WarehouseStock> _warehouseStock = new();
        public IReadOnlyCollection<WarehouseStock> WarehouseStock => _warehouseStock;



        private Product() { }

        private Product(
            Guid id,
            string sku,
            string? barCode,
            string productName,
            string? description,
            Guid categoryId,
            decimal sellingPrice,
            bool isActive,
            Unit unit) : base(id)
        {
            SKU = sku;
            BarCode = barCode;
            ProductName = productName;
            Description = description;
            CategoryId = categoryId;
            SellingPrice = sellingPrice;
            IsActive = isActive;
            Unit = unit;
        }

        public static Result<Product> Create(
            Guid id,
            string sku,
            string? barCode,
            string productName,
            string? description,
            Guid categoryId,
            decimal sellingPrice,
            bool isActive,
            Unit unit)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return ProductErrors.SKURequired;

            if (sku.Length > 10)
                return ProductErrors.SKUTooLong;

            if (!string.IsNullOrWhiteSpace(barCode) && barCode.Length > 50)
                return ProductErrors.BarCodeTooLong;

            if (string.IsNullOrWhiteSpace(productName))
                return ProductErrors.ProductNameRequired;

            if (productName.Length > 30)
                return ProductErrors.ProductNameTooLong;

            if (!string.IsNullOrWhiteSpace(description) && description.Length > 500)
                return ProductErrors.DescriptionTooLong;

            if (categoryId == Guid.Empty)
                return ProductErrors.CategoryRequired;

            if (sellingPrice < 0)
                return ProductErrors.InvalidPrice;

            if (!Enum.IsDefined(typeof(Unit), unit))
                return ProductErrors.InvalidUnit;

            var product = new Product(
                id,
                sku,
                barCode,
                productName,
                description,
                categoryId,
                sellingPrice,
                isActive,
                unit
            );

            return product;
        }
        
        public Result<Updated> Update(
            string sku,
            string? barCode,
            string productName,
            string? description,
            Guid categoryId,
            decimal sellingPrice,
            bool isActive,
            Unit unit)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return ProductErrors.SKURequired;

            if (sku.Length > 10)
                return ProductErrors.SKUTooLong;

            if (!string.IsNullOrWhiteSpace(barCode) && barCode.Length > 50)
                return ProductErrors.BarCodeTooLong;

            if (string.IsNullOrWhiteSpace(productName))
                return ProductErrors.ProductNameRequired;

            if (productName.Length > 30)
                return ProductErrors.ProductNameTooLong;

            if (!string.IsNullOrWhiteSpace(description) && description.Length > 500)
                return ProductErrors.DescriptionTooLong;

            if (categoryId == Guid.Empty)
                return ProductErrors.CategoryRequired;

            if (sellingPrice < 0)
                return ProductErrors.InvalidPrice;

            if (!Enum.IsDefined(typeof(Unit), unit))
                return ProductErrors.InvalidUnit;

            SKU = sku;
            BarCode = barCode;
            ProductName = productName;
            Description = description;
            CategoryId = categoryId;
            SellingPrice = sellingPrice;
            IsActive = isActive;
            Unit = unit;

            return Result.Updated;
        }

        public Result<Created> AddProductImage(ProductImage productImage) {

            if (productImage == null)
                return ProductErrors.InvalidProductImage;
            
            this._productImages.Add(productImage);
            return Result.Created;
        }


    } } 

