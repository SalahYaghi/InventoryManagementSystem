using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.ContactInfos.Commands.DeleteContactInfo
{
    public sealed record DeleteContactInfoCommand(Guid Id) : IRequest<Result<Deleted>>;
}

