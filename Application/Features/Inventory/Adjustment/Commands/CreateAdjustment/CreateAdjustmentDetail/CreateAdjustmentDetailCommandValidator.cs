using FluentValidation;

namespace Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail
{
    public sealed class CreateAdjustmentDetailInnerCommandValidator : AbstractValidator<CreateAdjustmentDetailInnerCommand>
    {
        public CreateAdjustmentDetailInnerCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}

