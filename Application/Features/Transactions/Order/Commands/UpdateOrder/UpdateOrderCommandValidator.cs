using FluentValidation;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.DiscountAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Notes).MaximumLength(500);
        }
    }
}

