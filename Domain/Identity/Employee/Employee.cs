using Domain.Identity.Users;
using Domain.People;
using Domain.Warehouses;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Domain.Identity.Employee
{
    public class Employee : AuditableEntity
    {

        public string JobTitle { get; set; }
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }
        public DateOnly HiringDate { get; set; }
     
        public Guid ?WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; } 
    
        private readonly List<User> _users = new List<User>();
        public IReadOnlyCollection<User> Users => _users;


        private Employee() { }
        private Employee(Guid id , string jobTitle, Person person,
            DateOnly hiringDate, Guid warehouseId) : base(id)
        {
            JobTitle = jobTitle;
            Person = person;
            this.PersonId = person.Id;
            HiringDate = hiringDate;
            this.WarehouseId = warehouseId;
        }

        private Employee(string jobTitle, Guid personId,
            DateOnly hiringDate , Guid warehouseId) : base(Guid.NewGuid()){

            JobTitle = jobTitle;
            PersonId = personId;
            HiringDate = hiringDate;
            this.WarehouseId = warehouseId;
        }
        public   Result<Updated> Update(string jobTitle,
         DateOnly hiringDate, Guid warehouseId)
        {

            if (warehouseId == Guid.Empty)
                return EmployeeErrors.WarehouseIsRequired;

            if (string.IsNullOrEmpty(jobTitle))
                return EmployeeErrors.JobTitleIsRequired;

            JobTitle = jobTitle;
             HiringDate = hiringDate;
            this.WarehouseId = warehouseId;return Result.Updated;
        }

        public static Result<Employee> Create(string jobTitle, Guid personId,
            DateOnly hiringDate , Guid warehouseId) {

            if ( warehouseId == Guid.Empty)
                return EmployeeErrors.WarehouseIsRequired;

            if (string.IsNullOrWhiteSpace(jobTitle))
                return EmployeeErrors.JobTitleIsRequired;

            if (Guid.Empty == personId)
                return EmployeeErrors.PersonIsRequired;

            return new Employee(jobTitle  , personId , hiringDate , 
                warehouseId);
        }

        public static Result<Employee> Create(string jobTitle, Person person,
            DateOnly hiringDate, Guid warehouseId)
        {

            if (warehouseId == Guid.Empty)
                return EmployeeErrors.WarehouseIsRequired;

            if (string.IsNullOrWhiteSpace(jobTitle))
                return EmployeeErrors.JobTitleIsRequired;

            if (person == null)
                return EmployeeErrors.PersonIsRequired;

            return new Employee(Guid.NewGuid() ,jobTitle, person, hiringDate,
                warehouseId);
        }





    }


}

