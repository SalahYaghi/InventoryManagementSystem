using Contract.Common;
using Contract.Requests.Addresses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Requests.Warehouses
{

    public class CreateWarehouseRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code is required.")]
        [MaxLength(50, ErrorMessage = "Code must not exceed 50 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "AddressId is required.")]
        public CreateAddressRequest Address { get; set; } = new();

    }
}


