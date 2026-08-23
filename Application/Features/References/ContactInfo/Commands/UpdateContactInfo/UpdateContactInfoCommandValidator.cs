using FluentValidation;

namespace Contract.Features.References.ContactInfos.Commands.UpdateContactInfo
{
    public sealed class UpdateContactInfoCommandValidator : AbstractValidator<UpdateContactInfoCommand>
    {
        public UpdateContactInfoCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(256);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.AlternitavePhoneNumber).MaximumLength(20);
            RuleFor(x => x.FaxNumber).MaximumLength(20);
            RuleFor(x => x.WebsiteUrl).MaximumLength(500);
        }
    }
}

