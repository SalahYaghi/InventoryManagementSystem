using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Transactions.Invoice.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Transactions.Invoice;

public class InvoiceMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Invoice();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.InvoiceId);
        Assert.Equal(entity.Status.ToString(), dto.Status);
        Assert.Equal(entity.InvoiceType.ToString(), dto.InvoiceType);
        Assert.Equal(entity.NetAmount, dto.NetAmount);
        Assert.Equal(entity.SubTotalAmount, dto.SubTotalAmount);
        Assert.Equal(entity.TaxAmount, dto.TaxAmount);
        Assert.Equal(entity.DiscountAmount, dto.DiscountAmount);
        Assert.Equal(entity.OrderId, dto.OrderId);
    }

    [Fact]
    public void ToDto_MapsLineItems()
    {
        var entity = MapperTestData.Invoice();
        var dto = entity.ToDto();
        Assert.Equal(entity.LineItems.Count, dto.InvoiceLineItems.Count);
        var src = entity.LineItems.First();
        var dest = dto.InvoiceLineItems.First();
        Assert.Equal(src.LineNo, dest.LineNo);
        Assert.Equal(src.Description, dest.Description);
        Assert.Equal(src.Quantity, dest.Quantity);
        Assert.Equal(src.UnitPrice, dest.UnitPrice);
        Assert.Equal(src.Tax, dest.Tax);
        Assert.Equal(src.TotalAmount, dest.TotalAmount);
    }

    [Fact]
    public void ToDto_LeavesOrderNull_WhenNotLoaded()
    {
        var dto = MapperTestData.Invoice().ToDto();
        Assert.Null(dto.Order);
    }
}
