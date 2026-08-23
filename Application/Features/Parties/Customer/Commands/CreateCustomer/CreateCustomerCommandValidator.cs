using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using FluentValidation;

namespace Contract.Features.Parties.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
             RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CustomerCode).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Contact)
               .NotNull()
               .WithMessage("Contacts are required")
               .SetValidator(new CreateContactInfoCommandValidator());
            RuleFor(x => x.Address)
                .NotNull()
                .WithMessage("Addresses are required")
                .SetValidator(new CreateAddressCommandValidator());


            RuleFor(x => x.Notes).MaximumLength(500);
        }
    }
}

