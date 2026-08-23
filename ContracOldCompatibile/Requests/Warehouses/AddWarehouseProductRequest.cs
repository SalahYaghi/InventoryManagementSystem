using Contract.Requests.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Requests.Warehouses
{

    public class AddWarehouseProductRequest
    {
        [Required(ErrorMessage = "Product is required.")]
        public CreateProductRequest Product { get; set; }

        public Guid WarehouseId { get; set; }

    }
}



