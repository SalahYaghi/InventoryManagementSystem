using MediatR;
using Inventory.Domain.Common.Results;
using System.Data;
using Contract.Features.Inventory.Adjustment.DTOs;

namespace Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail
{
    public sealed record CreateAdjustmentDetailInnerCommand : IRequest<Result<AdjustmentDetailDto>>
    {
        public Guid ProductId { get; init; }
        public decimal Quantity { get; init; }
        public byte[] RowVersion { get; init; } = [];
    }
}

