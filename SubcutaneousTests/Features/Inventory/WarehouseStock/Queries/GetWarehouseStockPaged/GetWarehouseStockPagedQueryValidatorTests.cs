using Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockPaged;

public class GetWarehouseStockPagedQueryValidatorTests
{
    private readonly GetWarehouseStockPagedQueryValidator _validator = new();

    [Fact] public void Validate_WithValidPaging_ShouldNotHaveValidationError() { var q = new GetWarehouseStockPagedQuery(Guid.NewGuid()) { PageNumber = 1, PageSize = 10 }; _validator.TestValidate(q).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithPageNumberLessThanMinimum_ShouldHaveValidationError() { var q = new GetWarehouseStockPagedQuery(Guid.NewGuid()) { PageNumber = 0, PageSize = 10 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageNumber); }
    [Fact] public void Validate_WithPageSizeTooSmall_ShouldHaveValidationError() { var q = new GetWarehouseStockPagedQuery(Guid.NewGuid()) { PageNumber = 1, PageSize = 0 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageSize); }
    [Fact] public void Validate_WithPageSizeTooLarge_ShouldHaveValidationError() { var q = new GetWarehouseStockPagedQuery(Guid.NewGuid()) { PageNumber = 1, PageSize = 101 }; _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.PageSize); }
}
