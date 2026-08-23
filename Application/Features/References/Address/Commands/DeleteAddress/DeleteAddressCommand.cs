using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.Addresses.Commands.DeleteAddress
{
    public sealed record DeleteAddressCommand(Guid Id) : IRequest<Result<Deleted>>;
}

