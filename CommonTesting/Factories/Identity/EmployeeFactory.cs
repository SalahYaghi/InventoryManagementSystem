using Domain.Identity.Employee;
using Domain.People;
using InventoryManagement.Tests.Common.Factories.People;

namespace InventoryManagement.Tests.Common.Factories.Identity;

public static class EmployeeFactory
{
    public static Employee CreateValid(string jobTitle = "Manager", Person? person = null, DateOnly? hiringDate = null, Guid? warehouseId = null)
    {
        var result = Employee.Create(jobTitle, person ?? PersonFactory.CreateValid(), hiringDate ?? new DateOnly(2024, 1, 1), warehouseId ?? Guid.NewGuid());
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }
}
