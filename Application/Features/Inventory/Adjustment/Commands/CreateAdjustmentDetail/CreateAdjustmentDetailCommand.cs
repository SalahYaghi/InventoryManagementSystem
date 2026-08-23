using Contract.Features.Inventory.Adjustment.DTOs;
using Contract.Features.Transactions.Order.DTOs;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Transactions.Order.Commands.CreateOrderDetail
{
   
    public sealed record CreateAdjustmentDetailCommand : IRequest<Result<AdjustmentDetailDto>>
    {
        public byte[] RowVersion { get; set; } = [];

        public Guid AdjustmentId { get; init; }
        public Guid ProductId { get; init; }
        public decimal Quantity { get; init; }
    }
}

