using Domain.Products;
using FluentAssertions;

namespace DomainTesting;

public class ProductTest
{
    [Fact]
    public void CreateProduct_WithValidData_ShouldSucceed()
    {
        // Arrange
        var productName = "Test Product";

        // Act
        var result = Product.Create(
            Guid.NewGuid(),
            "SKU-001",
            "BAR-001",
            productName,
            "Description",
            Guid.NewGuid(),
            13,
            false,
            Domain.Products.Enums.Unit.Piece);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.ProductName.Should().Be(productName);
    }
}