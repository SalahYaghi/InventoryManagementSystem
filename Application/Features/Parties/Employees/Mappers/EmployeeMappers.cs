using Contract.Features.Inventory.Warehouses.Mappers;
using Contract.Features.Parties.Employees.Dtos;
using Contract.Features.Parties.People.Mappers;
using Domain.Identity.Employee;

namespace Contract.Features.Parties.Employees.Mappers
{
    public static class EmployeeMappers
    {
        public static EmployeeDto ToDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,

                HiringDate = employee.HiringDate,
                JobTitle = employee.JobTitle,
                Person = employee.Person?.ToDto(),

                PersonId = employee.PersonId,

                Warehouse = employee.Warehouse?.ToDto(),
                WarehouseId = employee.WarehouseId
            };
        }
    }
}
