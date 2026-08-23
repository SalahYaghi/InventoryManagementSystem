using Domain.Identity.Employee;
using InventoryManagement.Application.DomainTesting.TestHelpers;
using EmployeeEntity = Domain.Identity.Employee.Employee;
using PersonEntity = Domain.People.Person;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Identity;

public class EmployeeTests
{
    private static PersonEntity CreateValidPerson() =>
        PersonEntity.Create(
            Guid.NewGuid(), "1234567890", "Ahmad", "Sami", null, "Yousef",
            true, new DateOnly(1990, 5, 1),
            TestData.ValidContact(), TestData.ValidAddress()).Value!;

    // ---------- Create (personId overload) ----------

    [Fact]
    public void Create_WithPersonId_Succeeds()
    {
        var personId = Guid.NewGuid();
        var warehouseId = Guid.NewGuid();
        var hiringDate = new DateOnly(2024, 1, 15);

        var result = EmployeeEntity.Create("Storekeeper", personId, hiringDate, warehouseId);

        Assert.False(result.IsError);
        var employee = result.Value!;
        Assert.Equal("Storekeeper", employee.JobTitle);
        Assert.Equal(personId, employee.PersonId);
        Assert.Equal(hiringDate, employee.HiringDate);
        Assert.Equal(warehouseId, employee.WarehouseId);
        Assert.NotEqual(Guid.Empty, employee.Id); // id is generated internally
    }

    [Fact]
    public void Create_WithEmptyWarehouseId_Fails()
    {
        var result = EmployeeEntity.Create("Storekeeper", Guid.NewGuid(), new DateOnly(2024, 1, 15), Guid.Empty);

        Assert.True(result.IsError);
        Assert.Equal(EmployeeErrors.WarehouseIsRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyPersonId_Fails()
    {
        var result = EmployeeEntity.Create("Storekeeper", Guid.Empty, new DateOnly(2024, 1, 15), Guid.NewGuid());

        Assert.True(result.IsError);
        Assert.Equal(EmployeeErrors.PersonIsRequired.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WithNullOrEmptyJobTitle_Fails(string? jobTitle)
    {
        var result = EmployeeEntity.Create(jobTitle!, Guid.NewGuid(), new DateOnly(2024, 1, 15), Guid.NewGuid());

        Assert.True(result.IsError);
        Assert.Equal(EmployeeErrors.JobTitleIsRequired.Code, result.TopError.Code);
    }

    // ⚠ BUG-EXPOSING TEST — Create uses IsNullOrEmpty instead of
    // IsNullOrWhiteSpace, so a job title of "   " is accepted.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Create_WithWhitespaceJobTitle_ShouldFail_ButIsAccepted()
    {
        var result = EmployeeEntity.Create("   ", Guid.NewGuid(), new DateOnly(2024, 1, 15), Guid.NewGuid());

        // EXPECTED: JobTitleIsRequired. ACTUAL: succeeds.
        Assert.True(result.IsError);
    }

    // ---------- Create (Person object overload) ----------

    [Fact]
    public void Create_WithPersonObject_Succeeds()
    {
        var person = CreateValidPerson();

        var result = EmployeeEntity.Create("Manager", person, new DateOnly(2024, 1, 15), Guid.NewGuid());

        Assert.False(result.IsError);
        Assert.Same(person, result.Value!.Person);
    }

    [Fact]
    public void Create_WithNullPersonObject_Fails()
    {
        var result = EmployeeEntity.Create("Manager", (PersonEntity)null!, new DateOnly(2024, 1, 15), Guid.NewGuid());

        Assert.True(result.IsError);
        Assert.Equal(EmployeeErrors.PersonIsRequired.Code, result.TopError.Code);
    }

    // ⚠ BUG-EXPOSING TEST — the Person-object constructor sets the Person
    // navigation but never sets PersonId, so PersonId stays Guid.Empty until
    // EF Core fixes it up at save time. In pure domain terms the aggregate is
    // inconsistent (PersonId disagrees with Person.Id) between Create and
    // SaveChanges. Fix: set PersonId = person.Id in the constructor.
    [Fact]
    [Trait("Category", "BugExposing")]
    public void Create_WithPersonObject_ShouldSetPersonId()
    {
        var person = CreateValidPerson();

        var employee = EmployeeEntity.Create("Manager", person, new DateOnly(2024, 1, 15), Guid.NewGuid()).Value!;

        // EXPECTED: PersonId mirrors person.Id. ACTUAL: Guid.Empty.
        Assert.Equal(person.Id, employee.PersonId);
    }

    // ---------- Update ----------

    [Fact]
    public void Update_WithValidData_Succeeds()
    {
        var employee = EmployeeEntity.Create(
            "Storekeeper", Guid.NewGuid(), new DateOnly(2024, 1, 15), Guid.NewGuid()).Value!;
        var newWarehouse = Guid.NewGuid();

        var result = employee.Update("Senior Storekeeper", new DateOnly(2025, 2, 1), newWarehouse);

        Assert.False(result.IsError);
        Assert.Equal("Senior Storekeeper", employee.JobTitle);
        Assert.Equal(new DateOnly(2025, 2, 1), employee.HiringDate);
        Assert.Equal(newWarehouse, employee.WarehouseId);
    }

    [Fact]
    public void Update_WithEmptyWarehouseId_FailsWithoutMutating()
    {
        var employee = EmployeeEntity.Create(
            "Storekeeper", Guid.NewGuid(), new DateOnly(2024, 1, 15), Guid.NewGuid()).Value!;

        var result = employee.Update("New Title", new DateOnly(2025, 2, 1), Guid.Empty);

        Assert.True(result.IsError);
        Assert.Equal(EmployeeErrors.WarehouseIsRequired.Code, result.TopError.Code);
        Assert.Equal("Storekeeper", employee.JobTitle);
    }

    // Design note (not a failing test): WarehouseId is nullable (Guid?) yet both
    // Create overloads and Update require a non-empty warehouse — the nullable
    // type suggests "optional" while the rules say "required". Also, hiring
    // dates are never range-checked (a hiring date centuries in the past or
    // future is accepted), and EmployeeErrors.PersonIsRequired uses the code
    // "Person.IsRequired" while WarehouseIsRequired uses "Warehouse.IsRequired" —
    // both under the Employee error catalog, which makes error filtering by
    // prefix inconsistent.
}
