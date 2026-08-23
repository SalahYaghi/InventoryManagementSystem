using FluentValidation;

namespace Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail
{
    public sealed class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}

