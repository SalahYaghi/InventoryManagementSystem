using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustment
{
    public class UpdateAdjustmentCommand : IRequest<Result<Updated>>
    {
        public Guid Id { get; set; }
        public string? Notes { get; init; }
    }
}

