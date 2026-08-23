using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity.Employee
{
    public class EmployeeErrors
    {
        public static Error JobTitleIsRequired => Error.Validation("Employee.JobTitelRequired",
            "Job title  is required can't be empty.");
        public static Error PersonIsRequired => Error.Validation("Person.IsRequired",
           "Person is required can't be empty.");

        public static Error EmployeeIsRequired => Error.Validation("Employee.IsRequired" , 
            "Employee is required can't be empty.");
        public static Error WarehouseIsRequired => Error.Validation("Warehouse.IsRequired",
            "Warehouse is required can't be empty.");
    }
}

