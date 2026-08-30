using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Countries.DTOs;

namespace Contract.Features.References.Countries.Commands.UpdateCountry
{
    public sealed record UpdateCountryCommand : IRequest<Result<CountryDto>>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}

