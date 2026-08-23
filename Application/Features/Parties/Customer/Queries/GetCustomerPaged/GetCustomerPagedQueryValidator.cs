using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Parties.Customers.Queries.GetCustomerPaged
{
    public sealed class GetCustomerPagedQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerPagedQueryValidator()
        {
          
        }
    }
}

