using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.References.Documents.Queries.GetDocumentPaged
{
    public sealed class GetDocumentPagedQueryValidator : AbstractValidator<GetDocumentPagedQuery>
    {
        public GetDocumentPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(ApplicationDefaults.DefaultPageNumber);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(ApplicationDefaults.MinimumPageSize, ApplicationDefaults.MaximumPageSize);
        }
    }
}

