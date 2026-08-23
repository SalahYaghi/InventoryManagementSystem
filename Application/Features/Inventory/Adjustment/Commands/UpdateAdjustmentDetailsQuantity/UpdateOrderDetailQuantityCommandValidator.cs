using Contract.Features.Inventory.Adjustment.Commands.UpdateAdjustmentDetailsQuantity;
using FluentValidation;

namespace Contract.Features.Transactions.Order.Commands.UpdateOrderDetail
{
    public sealed class UpdateAdjustmentDetailQuantityCommandValidator : AbstractValidator<UpdateAdjustmentDetailQuantityCommand>
    {
        public UpdateAdjustmentDetailQuantityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0); 
        }
    }
}

