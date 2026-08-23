using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Parties.People.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Dtos
{
    public class EmployeeDtoForList
    {
        public Guid EmployeeId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public Guid PersonId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalNo { get; set; }
        public DateOnly HiringDate { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public Guid? WarehouseId { get; set; }

    }
}

