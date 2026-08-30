using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Cities.DTOs;

namespace Contract.Features.References.Cities.Commands.UpdateCity
{
    public sealed record UpdateCityCommand : IRequest<Result<CityDto>>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

