using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Inventory.Product.Queries.GetProductPaged
{
    public sealed class GetProductPagedQueryValidator : AbstractValidator<GetProductPagedQuery>
    {
        public GetProductPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

