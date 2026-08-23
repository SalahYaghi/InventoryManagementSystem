using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Transactions.Orders.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Transactions.Order;

public class OrderMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.SaleOrder();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.OrderType, dto.OrderType);
        Assert.Equal(entity.OrderStatus, dto.OrderStatus);
        Assert.Equal(entity.SupplierId, dto.SupplierId);
        Assert.Equal(entity.CustomerId, dto.CustomerId);
        Assert.Equal(entity.InvoiceId, dto.InvoiceId);
        Assert.Equal(entity.SourceWarehouseId, dto.SourceWarehouseId);
        Assert.Equal(entity.DestinationWarehouseId, dto.DestinationWarehouseId);
        Assert.Equal(entity.NetAmount, dto.NetAmount);
        Assert.Equal(entity.SubTotalAmount, dto.SubTotalAmount);
        Assert.Equal(entity.DiscountAmount ?? 0, dto.DiscountAmount);
        Assert.Equal(entity.Notes, dto.Notes);
        Assert.Equal(entity.DueDate, dto.DueDate);
    }

    [Fact]
    public void ToDto_MapsOrderDetails()
    {
        var entity = MapperTestData.SaleOrder();
        var dto = entity.ToDto();
        Assert.Equal(entity.OrderDetails.Count, dto.OrderDetails.Count);
        var src = entity.OrderDetails.First();
        var dest = dto.OrderDetails.First();
        Assert.Equal(src.Id, dest.Id);
        Assert.Equal(src.Quantity, dest.Quantity);
        Assert.Equal(src.UnitPrice, dest.UnitPrice);
    }

    [Fact]
    public void ToDto_MapsCustomer_WhenLoaded()
    {
        var customer = MapperTestData.Customer();
        var dto = MapperTestData.SaleOrder(customer: customer).ToDto();
        Assert.NotNull(dto.Customer);
        Assert.Equal(customer.Id, dto.Customer!.Id);
    }

    [Fact]
    public void ToDto_MapsSourceWarehouse_WhenLoaded()
    {
        var wh = MapperTestData.Warehouse();
        var dto = MapperTestData.SaleOrder(sourceWarehouse: wh).ToDto();
        Assert.NotNull(dto.SourceWarehouseDto);
        Assert.Equal(wh.Id, dto.SourceWarehouseDto!.Id);
    }
}
