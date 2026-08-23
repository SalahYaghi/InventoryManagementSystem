using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.Documents.Commands.CreateDocument;
using FluentValidation;

namespace Contract.Features.Parties.People.Commands.CreatePerson
{
    public sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.NationalNo).NotEmpty().MaximumLength(20);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(10);
            RuleFor(x => x.SecondName).NotEmpty().MaximumLength(10);
            RuleFor(x => x.ThirdName).MaximumLength(10);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Contact).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();

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

