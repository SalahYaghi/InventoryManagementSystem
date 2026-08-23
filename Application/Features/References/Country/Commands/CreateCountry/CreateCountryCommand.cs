using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.References.Countries.DTOs;

namespace Contract.Features.References.Countries.Commands.CreateCountry
{
    public sealed record CreateCountryCommand : IRequest<Result<CountryDto>>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

