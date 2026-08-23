using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.Parties.People.DTOs;

namespace Contract.Features.Parties.Employees.Dtos
{
    public sealed class EmployeeDto
    {
        public Guid Id { get; set; }

        public string JobTitle { get; set; } = string.Empty;
        public Guid PersonId { get; set; }
        public PersonDto? Person { get; set; }
        public Guid? WarehouseId { get; set; }
        public WarehouseDto? Warehouse { get; set; }
        public DateOnly HiringDate { get; set; }
    }
}
