using FluentValidation;

namespace Contract.Features.Parties.SupplierProducts.Commands.UpdateSupplierProduct
{
    public sealed class UpdateSupplierProductCommandValidator : AbstractValidator<UpdateSupplierProductCommand>
    {
        public UpdateSupplierProductCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();

            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.PurchasePrice).GreaterThanOrEqualTo(0);
        }
    }
}

