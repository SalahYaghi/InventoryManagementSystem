using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustmentDetailsQuantity
{
    public class UpdateAdjustmentDetailQuantityCommand : IRequest<Result<Updated>>
    {
        public Guid Id { get;init;  }
        public decimal Quantity { get; init;  }
        public byte[] RowVersion { get; init ; } = [];

    }
}

