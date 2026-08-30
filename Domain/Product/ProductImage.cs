using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Products
{
    using global::Domain.Common.Helpers;
    using Inventory.Domain.Common;
    using Inventory.Domain.Common.Results;
    using System;

    namespace Domain.Products
    {
        public class ProductImage : AuditableEntity
        {
            public Guid ProductId { get; set; }
            public Product? Product { get; set; }
            public string ImageUrl { get; set; } = string.Empty;

            private ProductImage() { }

            private ProductImage(Guid id, Guid productId, string imageUrl) : base(id)
            {
                ProductId = productId;
                ImageUrl = imageUrl;
            }

            public static Result<ProductImage> Create(Guid id, Guid productId, string imageUrl)
            {
                if (productId == Guid.Empty)
                    return ProductImageErrors.ProductRequired;

                if (string.IsNullOrWhiteSpace(imageUrl))
                    return ProductImageErrors.ImageUrlRequired;

                if (!ValidationHelper.IsValidImageUrlOrPath(imageUrl))
                    return ProductImageErrors.ImageUrlInvalid;

                var productImage = new ProductImage(id, productId, imageUrl);

                return productImage;
            }
        
            public Result<Updated> Update(Guid productId, string imageUrl)
            {
                if (productId == Guid.Empty)
                    return ProductImageErrors.ProductRequired;

                if (string.IsNullOrWhiteSpace(imageUrl))
                    return ProductImageErrors.ImageUrlRequired;

                if (!ValidationHelper.IsValidImageUrlOrPath(imageUrl))
                    return ProductImageErrors.ImageUrlInvalid;

                ProductId = productId;
                ImageUrl = imageUrl;

                return Result.Updated;
            }

        }
    } }

