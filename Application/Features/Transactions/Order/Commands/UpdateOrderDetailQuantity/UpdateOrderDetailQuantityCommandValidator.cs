using FluentValidation;

namespace Contract.Features.Transactions.Order.Commands.UpdateOrderDetail
{
    public sealed class UpdateOrderDetailQuantityCommandValidator : AbstractValidator<UpdateOrderDetailCommand>
    {
        public UpdateOrderDetailQuantityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0); 
        }
    }
}

