using Contract.Features.Inventory.Product.Commands.CreateProduct;
using Contract.Features.Inventory.WarehouseStock.Commands.AddWarehouseProducts;
using Contract.Features.Inventory.WarehouseStocks.Commands.UpdateWarehouseStock;
using Domain.Products.Enums;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Inventory.WarehouseStock.Commands.CreateWarehouseStock;

public class AddWarehourProductCommondValidatorTests
{
    private readonly AddWarehourProductCommondValidator _validator = new();

    private static AddWarehourProductCommand ValidCommand() => new()
    {
        WarehousesId = Guid.NewGuid(),
        Product = new CreateProductCommand
        {
            SKU = "SKU-OK",
            BarCode = "BAR-OK",
            ProductName = "Engine Oil",
            SellingPrice = 10m,
            IsActive = true,
            Unit = Domain.Products.Enums.Unit.Piece,
            CategoryId = Guid.NewGuid()
        }
    };

    [Fact] public void Validate_WithValidData_ShouldNotHaveValidationError() => _validator.TestValidate(ValidCommand()).ShouldNotHaveAnyValidationErrors();
    [Fact] public void Validate_WithEmptyWarehouseId_ShouldHaveValidationError() { var c = ValidCommand(); c.WarehousesId = Guid.Empty; _validator.TestValidate(c).ShouldHaveValidationErrorFor(x => x.WarehousesId); }
    [Fact] public void Validate_WithEmptyProductSku_ShouldHaveValidationError() { var c = ValidCommand(); c.Product = c.Product with { SKU = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor("Product.SKU"); }
    [Fact] public void Validate_WithTooLongProductSku_ShouldHaveValidationError() { var c = ValidCommand(); c.Product = c.Product with { SKU = new string('S', 11) }; _validator.TestValidate(c).ShouldHaveValidationErrorFor("Product.SKU"); }
    [Fact] public void Validate_WithEmptyProductName_ShouldHaveValidationError() { var c = ValidCommand(); c.Product = c.Product with { ProductName = string.Empty }; _validator.TestValidate(c).ShouldHaveValidationErrorFor("Product.ProductName"); }
    [Fact] public void Validate_WithNegativeSellingPrice_ShouldHaveValidationError() { var c = ValidCommand(); c.Product = c.Product with { SellingPrice = -1m }; _validator.TestValidate(c).ShouldHaveValidationErrorFor("Product.SellingPrice"); }
}

public class UpdateWarehouseStockMinimumLevelCommandValidatorTests
{
    private readonly UpdateWarehouseStockMinimumLevelCommandValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldNotHaveValidationError()
    {
        var command = new UpdateWarehouseStockMinimumLevelCommand { Id = Guid.NewGuid(), MinimumStockLevel = 5m };
        _validator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithEmptyId_ShouldHaveValidationError()
    {
        var command = new UpdateWarehouseStockMinimumLevelCommand { Id = Guid.Empty, MinimumStockLevel = 5m };
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_WithNegativeMinimumStockLevel_ShouldHaveValidationError()
    {
        var command = new UpdateWarehouseStockMinimumLevelCommand { Id = Guid.NewGuid(), MinimumStockLevel = -1m };
        _validator.TestValidate(command).ShouldHaveValidationErrorFor(x => x.MinimumStockLevel);
    }

    [Fact]
    public void Validate_WithZeroMinimumStockLevel_ShouldNotHaveValidationError()
    {
        var command = new UpdateWarehouseStockMinimumLevelCommand { Id = Guid.NewGuid(), MinimumStockLevel = 0m };
        _validator.TestValidate(command).ShouldNotHaveValidationErrorFor(x => x.MinimumStockLevel);
    }
}
