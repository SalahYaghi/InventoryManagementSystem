using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.Cities.Commands.DeleteCity
{
    public sealed record DeleteCityCommand(Guid Id) : IRequest<Result<Deleted>>;
}

