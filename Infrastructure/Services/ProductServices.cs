using Contract.Common.Errors;
using Contract.Common.Interfaces;
using MechanicShop.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class ProductServices(IAppDbContext context) : IProductPolicies
    {
        public async Task<Result<bool>> CheckSupplierSellsProducts(
            Guid supplierId, Guid[] products, CancellationToken ct)
        {
            var distinctProductIds = products.Distinct().ToArray();

            var supplierProductsCount = await context.SupplierProducts
                .Where(s => s.SupplierId == supplierId &&
                distinctProductIds.Contains(s.ProductId)).CountAsync(ct);

            if (supplierProductsCount != distinctProductIds.Length) {


                return ApplicationErrors.SupplierDoesNotSellProduct;

            
            }

            return true;
        }
    }
}

