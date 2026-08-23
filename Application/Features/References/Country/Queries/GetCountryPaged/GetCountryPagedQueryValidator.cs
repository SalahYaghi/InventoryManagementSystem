using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.References.Countries.Queries.GetCountryPaged
{
    public sealed class GetCountryPagedQueryValidator : AbstractValidator<GetCountryPagedQuery>
    {
        public GetCountryPagedQueryValidator()
        {
            }
    }
}

