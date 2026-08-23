using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Transactions.Order.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailCommandValidator() {

            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.OrderId).NotEmpty();


        }
    }
}

