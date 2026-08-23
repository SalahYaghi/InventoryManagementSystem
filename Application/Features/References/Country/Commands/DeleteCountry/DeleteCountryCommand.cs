using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.Countries.Commands.DeleteCountry
{
    public sealed record DeleteCountryCommand(Guid Id) : IRequest<Result<Deleted>>;
}

