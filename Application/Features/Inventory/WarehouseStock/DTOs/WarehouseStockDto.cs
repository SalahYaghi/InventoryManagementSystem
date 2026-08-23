namespace Contract.Features.Inventory.WarehouseStocks.DTOs
{
    public sealed record WarehouseStockDto
    {
        public Guid Id { get; init; }
        public Guid WarehouseId { get; init; }
        public Guid ProductId { get; init; }
        public decimal Quantity { get; init; }
        public decimal MinimumStockLevel { get; init; }
        public byte[] RowVersion { get; init; } = [];
    }
}

