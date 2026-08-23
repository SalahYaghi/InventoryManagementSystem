using FluentValidation;

namespace Contract.Features.References.ContactInfos.Commands.CreateContactInfo
{
    public sealed class CreateContactInfoCommandValidator : AbstractValidator<CreateContactInfoCommand>
    {
        public CreateContactInfoCommandValidator()
        {
             RuleFor(x => x.Email).NotEmpty().MaximumLength(256);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.AlternitavePhoneNumber).MaximumLength(20);
            RuleFor(x => x.FaxNumber).MaximumLength(20);
            RuleFor(x => x.WebsiteUrl).MaximumLength(500);
        }
    }
}

