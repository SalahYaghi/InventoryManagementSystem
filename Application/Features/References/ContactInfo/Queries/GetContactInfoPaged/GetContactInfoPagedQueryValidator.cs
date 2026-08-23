using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.References.ContactInfos.Queries.GetContactInfoPaged
{
    public sealed class GetContactInfoPagedQueryValidator : AbstractValidator<GetContactInfoPagedQuery>
    {
        public GetContactInfoPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

