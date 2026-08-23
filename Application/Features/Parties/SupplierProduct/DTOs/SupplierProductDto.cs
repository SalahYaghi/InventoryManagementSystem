namespace Contract.Features.Parties.SupplierProducts.DTOs
{
    public sealed record SupplierProductDto
    {
        public Guid Id { get; init; }
        public Guid SupplierId { get; init; }
        public Guid ProductId { get; init; }
        public decimal PurchasePrice { get; init; }
        public bool IsActive { get; init; }
    }
}

