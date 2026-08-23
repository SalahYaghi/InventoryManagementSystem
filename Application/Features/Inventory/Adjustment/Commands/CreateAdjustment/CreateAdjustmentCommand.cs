using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Adjustments.DTOs;
using Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail;

namespace Contract.Features.Inventory.Adjustments.Commands.CreateAdjustment
{
    public sealed record CreateAdjustmentCommand : IRequest<Result<AdjustmentDto>>
    {
        public Guid WarehouseId { get; init; }
        public Domain.Adjustments.AdjustmentType? AdjustmentType { get; init; }
        public Domain.Adjustments.AdjustmentReason AdjustmentReason { get; init; }
        public string? Notes { get; init; }
        public List<CreateAdjustmentDetailInnerCommand> AdjustmentDetailCommands { get; init; } = new();
    
    }
}

