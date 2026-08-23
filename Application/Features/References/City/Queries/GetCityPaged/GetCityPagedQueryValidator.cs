using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.References.Cities.Queries.GetCityPaged
{
    public sealed class GetCityPagedQueryValidator : AbstractValidator<GetCityByCountryIdPagedQuery>
    {
        public GetCityPagedQueryValidator()
        {

            RuleFor(x => x.CountryId)
                    .NotEmpty();
        }
    }
}

