using FluentValidation;

namespace Contract.Features.Transactions.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.OrderStatus).NotEmpty();
        }
    }
}

