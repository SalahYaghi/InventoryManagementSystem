using Contract.Features.References.Cities.DTOs;
using Contract.Features.References.Countries.DTOs;
using Domain.Contacts.Address.Country;

namespace Contract.Features.References.Addresses.DTOs
{
    public sealed record AddressDto
    {
        public Guid Id { get; init; }
        public Guid CountryId { get; init; }
        public Guid CityId { get; init; }
        public string? PostalCode { get; init; }
        public string? BuildingNumber { get; init; }
        public string? Street { get; init; }
        public string? Description { get; init; }

        public CityDto ? City { get; init; }
        public CountryDto? Country { get; init; }
    }
}

