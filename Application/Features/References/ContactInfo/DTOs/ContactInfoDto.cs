namespace Contract.Features.References.ContactInfos.DTOs
{
    public sealed record ContactInfoDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string? AlternitavePhoneNumber { get; init; }
        public string? FaxNumber { get; init; }
        public string? WebsiteUrl { get; init; }
    }
}

