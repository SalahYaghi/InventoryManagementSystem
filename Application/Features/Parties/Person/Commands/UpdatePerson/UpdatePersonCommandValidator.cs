using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using Contract.Features.References.Documents.Commands.UpdateDocument;
using FluentValidation;

namespace Contract.Features.Parties.People.Commands.UpdatePerson
{
    public sealed class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.NationalNo).NotEmpty().MaximumLength(20);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(10);
            RuleFor(x => x.SecondName).NotEmpty().MaximumLength(10);
            RuleFor(x => x.ThirdName).MaximumLength(10);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(10);

            RuleFor(x => x.Contact)
                .SetValidator(new UpdateContactInfoCommandValidator())
                .When(c => c.Contact is not null);

            RuleFor(x => x.Address)
                .SetValidator(new UpdateAddressCommandValidator())
                .When(c => c.Address is not null);
       
        
        }
    }
}

