using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged
{
    public sealed class GetWarehouseStockPagedQueryValidator : AbstractValidator<GetWarehouseStockPagedQuery>
    {
        public GetWarehouseStockPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

