using Contract.Features.Inventory.Warehouses.Queries.GetWarehousePaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.Warehouse.Queries.GetWarehousePaged;

public class GetWarehousePagedQueryValidatorTests
{
    private readonly GetWarehousePagedQueryValidator _validator = new();

    [Fact]
    public void Validate_WithDefaultQuery_ShouldNotHaveValidationError()
    {
        var query = new GetWarehousesQuery();
        _validator.TestValidate(query).ShouldNotHaveAnyValidationErrors();
    }
}
