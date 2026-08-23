using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Products
{
   
        public static class ProductImageErrors
        {
            public static readonly Error ProductRequired = Error.Validation(
                "ProductImage.ProductRequired",
                "Product is required.");

            public static readonly Error ImageUrlRequired = Error.Validation(
                "ProductImage.ImageUrlRequired",
                "Image URL is required.");

            public static readonly Error ImageUrlInvalid = Error.Validation(
                "ProductImage.ImageUrlInvalid",
                "Image URL is invalid.");
        }
}

