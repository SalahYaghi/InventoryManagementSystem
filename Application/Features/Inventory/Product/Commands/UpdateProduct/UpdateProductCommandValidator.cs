using FluentValidation;

namespace Contract.Features.Inventory.Product.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SKU).NotEmpty().MaximumLength(10);
            RuleFor(x => x.BarCode).MaximumLength(50);
            RuleFor(x => x.ProductName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.SellingPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}

