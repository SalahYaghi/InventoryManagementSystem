using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.References.Addresses.DTOs;

namespace Contract.Features.References.Addresses.Commands.UpdateAddress
{
    public sealed record UpdateAddressCommand : IRequest<Result<AddressDto>>
    {
        public Guid? Id { get; init; }
        public Guid CountryId { get; init; }
        public Guid CityId { get; init; }
        public string? PostalCode { get; init; }
        public string? BuildingNumber { get; init; }
        public string? Street { get; init; }
        public string? Description { get; init; }
    }
}

