using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using FluentValidation;

namespace Contract.Features.Parties.Customers.Commands.UpdateCustomer
{
    public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CustomerCode).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Notes).MaximumLength(500);

            RuleFor(x => x.Contact)
                .SetValidator(new UpdateContactInfoCommandValidator())
                .When(c => c.Contact is not null);

            RuleFor(x => x.Address)
                .SetValidator(new UpdateAddressCommandValidator())
                .When(c => c.Address is not null);
        }
    }
}

