using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Products
{
    public static class ProductErrors
    {
        public static readonly Error SKURequired = Error.Validation(
                "Product.SKURequired",
                "SKU is required.");

            public static readonly Error SKUTooLong = Error.Validation(
                "Product.SKUTooLong",
                "SKU exceeds maximum length.");

            public static readonly Error BarCodeTooLong = Error.Validation(
                "Product.BarCodeTooLong",
                "Barcode exceeds maximum length.");

            public static readonly Error ProductNameRequired = Error.Validation(
                "Product.ProductNameRequired",
                "Product name is required.");

            public static readonly Error ProductNameTooLong = Error.Validation(
                "Product.ProductNameTooLong",
                "Product name exceeds maximum length.");

            public static readonly Error DescriptionTooLong = Error.Validation(
                "Product.DescriptionTooLong",
                "Description exceeds maximum length.");

            public static readonly Error CategoryRequired = Error.Validation(
                "Product.CategoryRequired",
                "Category is required.");

            public static readonly Error InvalidPrice = Error.Validation(
                "Product.InvalidPrice",
                "Selling price must be greater than or equal to zero.");

            public static readonly Error InvalidUnit = Error.Validation(
                "Product.InvalidUnit",
                "Unit is invalid.");

        public static readonly Error InvalidProductImage = Error.Validation(
            "ProductImage.InvalidProductImage",
            "Product Image is invalid.");
    }
    } 
