using FluentValidation;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed class UpdateAdjustmentValidator : AbstractValidator<UpdateAdjustmentStatusCommand>
    {
        public UpdateAdjustmentValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.AdjustmentStatus).NotEmpty();
        }
    }
}

