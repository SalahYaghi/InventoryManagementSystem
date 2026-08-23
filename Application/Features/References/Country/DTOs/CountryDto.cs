namespace Contract.Features.References.Countries.DTOs
{
    public sealed record CountryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

