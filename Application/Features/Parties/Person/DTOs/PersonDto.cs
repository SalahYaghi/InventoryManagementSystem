using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.ContactInfos.DTOs;
using Contract.Features.References.Documents.DTOs;

namespace Contract.Features.Parties.People.DTOs
{
    public sealed record PersonDto
    {
        public Guid Id { get; init; }
        public string NationalNo { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string SecondName { get; init; } = string.Empty;
        public string? ThirdName { get; init; }
        public string LastName { get; init; } = string.Empty;
        public bool Gender { get; init; }
        public DateOnly DateOfBirth { get; init; }
        public Guid ContactId { get; init; }
        public Guid AddressId { get; init; }
        public Guid? DocumentId { get; init; }

        public ContactInfoDto? Contact { get; init; } = new();
        public DocumentDto? Document { get; init; } = new();
        public AddressDto? Address { get; init; } = new();
    }
}

