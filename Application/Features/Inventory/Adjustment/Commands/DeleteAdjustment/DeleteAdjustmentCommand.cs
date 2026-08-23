using MediatR;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Inventory.Adjustments.Commands.DeleteAdjustment
{
    public sealed record DeleteAdjustmentCommand(Guid Id) : IRequest<Result<Deleted>>;
}

