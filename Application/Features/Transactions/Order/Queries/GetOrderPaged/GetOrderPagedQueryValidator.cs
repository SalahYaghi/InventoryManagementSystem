using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Transactions.Orders.Queries.GetOrderPaged
{
    public sealed class GetOrderPagedQueryValidator : AbstractValidator<GetOrderPagedQuery>
    {
        public GetOrderPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

