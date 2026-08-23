using System;
using Contract.Common;

namespace Contract.Responses
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string BarCode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsActive { get; set; }
        public  Unit Unit { get; set; }
        public Guid CategoryId { get; set; }
         public CategoryDto  Category { get; init; }


    }
}


