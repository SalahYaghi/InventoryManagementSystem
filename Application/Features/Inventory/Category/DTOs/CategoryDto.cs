namespace Contract.Features.Inventory.Categories.DTOs
{
    public sealed record CategoryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

