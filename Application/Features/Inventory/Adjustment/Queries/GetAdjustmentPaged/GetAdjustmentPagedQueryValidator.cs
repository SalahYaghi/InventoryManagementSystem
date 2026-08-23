using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Inventory.Adjustments.Queries.GetAdjustmentPaged
{
    public sealed class GetAdjustmentPagedQueryValidator : AbstractValidator<GetAdjustmentPagedQuery>
    {
        public GetAdjustmentPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

