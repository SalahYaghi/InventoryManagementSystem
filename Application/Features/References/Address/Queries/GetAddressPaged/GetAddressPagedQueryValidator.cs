using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.References.Addresses.Queries.GetAddressPaged
{
    public sealed class GetAddressPagedQueryValidator : AbstractValidator<GetAddressPagedQuery>
    {
        public GetAddressPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

