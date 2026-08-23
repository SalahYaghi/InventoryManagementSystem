using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Transactions.Order.Queries.GetOrderDetailPaged
{
    public sealed class GetOrderDetailPagedQueryValidator : AbstractValidator<GetOrderDetailPagedQuery>
    {
        public GetOrderDetailPagedQueryValidator()
        {

            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}

