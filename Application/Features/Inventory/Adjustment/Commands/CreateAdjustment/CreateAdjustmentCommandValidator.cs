using Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail;
using FluentValidation;

namespace Contract.Features.Inventory.Adjustments.Commands.CreateAdjustment
{
    public sealed class CreateAdjustmentCommandValidator : AbstractValidator<CreateAdjustmentCommand>
    {
        public CreateAdjustmentCommandValidator()
        {
            RuleFor(x => x.WarehouseId).NotEmpty();
            RuleFor(x => x.Notes).MaximumLength(500);

            RuleFor(x => x.AdjustmentDetailCommands)
                .NotEmpty()
                .WithMessage("An adjustment must contain at least one detail line.");

            RuleForEach(x => x.AdjustmentDetailCommands)
                .SetValidator(new CreateAdjustmentDetailInnerCommandValidator());
        }
    }
}
