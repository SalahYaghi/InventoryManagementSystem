using FluentValidation;

namespace Contract.Features.Transactions.Invoice.Commands.CreateInvoice
{
    public sealed class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
         
        }
    }
}

