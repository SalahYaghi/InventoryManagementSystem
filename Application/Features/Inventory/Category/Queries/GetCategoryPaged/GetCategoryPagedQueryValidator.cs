using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Inventory.Categories.Queries.GetCategoryPaged
{
    public sealed class GetCategoryPagedQueryValidator : AbstractValidator<GetCategoryPagedQuery>
    {
        public GetCategoryPagedQueryValidator()
        {
         }
    }
}

