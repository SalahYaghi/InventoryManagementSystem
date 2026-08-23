using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Responses
{
    public class EmployeeDto
    {

        public string JobTitle { get; set; } = string.Empty;
       
        public Guid PersonId { get; set; }
        public PersonDto? Person { get; set; }
        
        public DateOnly HiringDate { get; set; }

        public Guid WarehouseId { get; set; }
        public WarehouseDto? Warehouse { get; set; }



    }
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

