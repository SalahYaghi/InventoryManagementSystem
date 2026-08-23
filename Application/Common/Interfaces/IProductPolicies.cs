using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface IProductPolicies
    {

        Task<Result<bool>> CheckSupplierSellsProducts(Guid supplierId , Guid[] products, CancellationToken ct);

    }
}

