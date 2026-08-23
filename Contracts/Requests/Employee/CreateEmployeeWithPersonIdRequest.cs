using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Employee
{
    public class CreateEmployeeWithPersonIdRequest
    {
        public string jobTitle { get; set; } = string.Empty; 
        public Guid personId { get; set; }
        public DateOnly hiringDate { get; set; }
        public Guid warehouseId { get; set; }   

    }

}

