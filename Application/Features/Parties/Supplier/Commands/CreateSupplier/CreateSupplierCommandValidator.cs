using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using FluentValidation;

namespace Contract.Features.Parties.Supplier.Commands.CreateSupplier
{
    public sealed class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SupplierName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SupplierCode).NotEmpty().MaximumLength(50);
             RuleFor(x => x.Notes).MaximumLength(500);
            RuleFor(x => x.Contact)
   .NotNull()
   .WithMessage("Contacts are required")
   .SetValidator(new CreateContactInfoCommandValidator());
            RuleFor(x => x.Address)
                .NotNull()
                .WithMessage("Addresses are required")
                .SetValidator(new CreateAddressCommandValidator());

        }
    }
}

