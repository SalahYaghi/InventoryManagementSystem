using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Inventory.Warehouses.Queries.GetWarehousePaged
{
    public sealed class GetWarehousePagedQueryValidator : AbstractValidator<GetWarehousesQuery>
    {
        public GetWarehousePagedQueryValidator()
        {
           }
    }
}

