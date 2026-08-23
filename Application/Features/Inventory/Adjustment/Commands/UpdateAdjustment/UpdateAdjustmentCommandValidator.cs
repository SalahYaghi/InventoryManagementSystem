using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustment;
using FluentValidation;

namespace Contract.Features.Inventory.Adjustments.Commands.UpdateAdjustment
{
    public sealed class UpdateAdjustmentCommandValidator : AbstractValidator<UpdateAdjustmentCommand>
    {
        public UpdateAdjustmentCommandValidator()
        {
              RuleFor(x => x.Notes).MaximumLength(500);
            RuleFor(x => x.Id).NotEmpty();

        }
    }
}

