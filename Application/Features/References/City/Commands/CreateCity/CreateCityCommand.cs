using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.References.Cities.DTOs;

namespace Contract.Features.References.Cities.Commands.CreateCity
{
    public sealed record CreateCityCommand : IRequest<Result<CityDto>>
    {
        public Guid Id { get; init; }
        public Guid CountryId { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

