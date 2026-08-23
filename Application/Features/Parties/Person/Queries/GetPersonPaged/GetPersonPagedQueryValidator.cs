using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Parties.People.Queries.GetPersonPaged
{
    public sealed class GetPersonPagedQueryValidator : AbstractValidator<GetPersonPagedQuery>
    {
        public GetPersonPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

