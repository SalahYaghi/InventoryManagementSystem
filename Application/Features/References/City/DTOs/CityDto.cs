namespace Contract.Features.References.Cities.DTOs
{
    public sealed record CityDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

