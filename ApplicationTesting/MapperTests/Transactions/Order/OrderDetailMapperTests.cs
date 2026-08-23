using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Transactions.Order.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Transactions.Order;

public class OrderDetailMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.OrderDetail();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.OrderId, dto.OrderId);
        Assert.Equal(entity.ProductId, dto.ProductId);
        Assert.Equal(entity.Quantity, dto.Quantity);
        Assert.Equal(entity.ActualQuantity, dto.ActualQuantity);
        Assert.Equal(entity.UnitPrice, dto.UnitPrice);
        Assert.Equal(entity.RowVersion, dto.RowVersion);
    }

    [Fact]
    public void ToDto_MapsProduct_WhenLoaded()
    {
        var product = MapperTestData.Product();
        var dto = MapperTestData.OrderDetail(product).ToDto();
        Assert.NotNull(dto.Product);
        Assert.Equal(product.Id, dto.Product!.Id);
    }

    [Fact]
    public void ToDto_LeavesProductNull_WhenNotLoaded()
    {
        var dto = MapperTestData.OrderDetail().ToDto();
        Assert.Null(dto.Product);
    }
}
