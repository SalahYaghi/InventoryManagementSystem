using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Contract.Requests.Addresses;

namespace Contract.Requests.Warehouses
{

    public class UpdateWarehouseRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code is required.")]
        [MaxLength(50, ErrorMessage = "Code must not exceed 50 characters.")]
        public string Code { get; set; } = string.Empty;

        public UpdateAddressRequest Address { get; set; }

        public int WarehouseStatus { get; set; }
    }
}


