using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Parties.SupplierProducts.Queries.GetSupplierProductPaged
{
    public sealed class GetSupplierProductPagedQueryValidator : AbstractValidator<GetSupplierProductsPagedQuery>
    {
        public GetSupplierProductPagedQueryValidator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty()
                .WithMessage("Supplier is required"); 
        }
    }
}

