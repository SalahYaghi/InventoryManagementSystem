using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface IOrderPolicies
    {

        Task<Result<bool>> CheckPrductAvailableQuantity(Guid warehouseId , Guid productId , decimal quantity,CancellationToken ct); 
    }
}

