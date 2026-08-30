using Application.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById;

public class GetWarehouseQueryStockByIdValidatorTests
{
    private readonly GetWarehouseQueryStockByIdValidator _validator = new();

    [Fact] public void Validate_WithValidId_ShouldNotHaveValidationError() { var q = new GetWarehouseStockByIdQuery(Guid.NewGuid()); _validator.TestValidate(q).ShouldNotHaveAnyValidationErrors(); }
    [Fact] public void Validate_WithEmptyId_ShouldHaveValidationError() { var q = new GetWarehouseStockByIdQuery(Guid.Empty); _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.Id); }
    [Fact] public void Validate_WithEmptyId_ShouldReturnExpectedMessage() { var q = new GetWarehouseStockByIdQuery(Guid.Empty); _validator.TestValidate(q).ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Invalid Id Sent."); }
}
