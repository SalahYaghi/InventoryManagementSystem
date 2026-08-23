using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Requests.Warehouses
{
    public class UpdateWarehouseStockMinimumLevelRequest
    {
        [Range(0, double.MaxValue, ErrorMessage = "MinimumStockLevel must be greater than or equal to 0.")]
        public decimal MinimumStockLevel { get; set; }
    }
}



