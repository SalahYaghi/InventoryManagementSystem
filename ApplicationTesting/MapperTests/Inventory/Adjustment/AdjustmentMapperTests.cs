using Application.UnitTests.Mappers.TestHelpers;
using Contract.Features.Inventory.Adjustments.Mappers;
using Contract.Features.Inventory.Adjustment.Mappers;
using Xunit;

namespace Application.UnitTests.Mappers.Inventory.Adjustment;

public class AdjustmentMapperTests
{
    [Fact]
    public void ToDto_MapsAllScalarProperties()
    {
        var entity = MapperTestData.Adjustment();
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.WarehouseId, dto.WarehouseId);
        Assert.Equal(entity.AdjustmentType, dto.AdjustmentType);
        Assert.Equal(entity.AdjustmentReason, dto.AdjustmentReason);
        Assert.Equal(entity.AdjustmentStatus, dto.AdjustmentStatus);
        Assert.Equal(entity.Notes, dto.Notes);
    }

    [Fact]
    public void ToDto_MapsWarehouse_WhenLoaded()
    {
        var wh = MapperTestData.Warehouse();
        var entity = MapperTestData.Adjustment(wh);
        var dto = entity.ToDto();
        Assert.NotNull(dto.Warehouse);
        Assert.Equal(wh.Id, dto.Warehouse!.Id);
    }

    [Fact]
    public void ToDto_LeavesWarehouseNull_WhenNotLoaded()
    {
        var dto = MapperTestData.Adjustment().ToDto();
        Assert.Null(dto.Warehouse);
    }

    [Fact]
    public void ToDto_MapsDetailList()
    {
        var entity = MapperTestData.Adjustment();
        var dto = entity.ToDto();
        Assert.Equal(entity.AdjustmentDetails.Count, dto.AdjustmentDetailDtos.Count);
        var src = entity.AdjustmentDetails.First();
        var dest = dto.AdjustmentDetailDtos.First();
        Assert.Equal(src.Id, dest.Id);
        Assert.Equal(src.ProductId, dest.ProductId);
        Assert.Equal(src.Quantity, dest.Quantity);
    }

    [Fact]
    public void AdjustmentDetailToDto_MapsAllProperties()
    {
        var product = MapperTestData.Product();
        var entity = MapperTestData.AdjustmentDetail(product);
        var dto = entity.ToDto();
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.AdjustmentId, dto.AdjustmentId);
        Assert.Equal(entity.ProductId, dto.ProductId);
        Assert.Equal(entity.Quantity, dto.Quantity);
        Assert.NotNull(dto.Product);
        Assert.Equal(product.Id, dto.Product!.Id);
    }

    [Fact]
    public void AdjustmentDetailToDto_LeavesProductNull_WhenNotLoaded()
    {
        var dto = MapperTestData.AdjustmentDetail().ToDto();
        Assert.Null(dto.Product);
    }
}
