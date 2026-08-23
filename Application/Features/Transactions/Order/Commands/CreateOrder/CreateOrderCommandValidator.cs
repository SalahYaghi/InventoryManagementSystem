using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
using FluentValidation;

namespace Contract.Features.Transactions.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Notes).MaximumLength(500);
            RuleFor(x => x.OrderType).NotEmpty();

       


            RuleFor(x => x.SourceWarehouseId)
                .Must(x =>  x != Guid.Empty)
                .WithMessage("DestinationWarehouseId id invalid value");

            RuleForEach(x => x.OrderDetails)
                .SetValidator(new CreateOrderDetailCommandValidator());
        }

    }

}

