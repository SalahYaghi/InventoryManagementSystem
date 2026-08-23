using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Parties.People.Commands.DeletePerson
{
    public sealed record DeletePersonCommand(Guid Id) : IRequest<Result<Deleted>>;
}

