using FluentValidation;

namespace Contract.Features.Parties.SupplierProducts.Commands.CreateSupplierProduct
{
    public sealed class CreateSupplierProductCommandValidator : AbstractValidator<CreateSupplierProductCommand>
    {
        public CreateSupplierProductCommandValidator()
        {
             RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.PurchasePrice).GreaterThanOrEqualTo(0);
        }
    }
}

