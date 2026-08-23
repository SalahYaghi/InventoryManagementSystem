using FluentValidation;

namespace Contract.Features.References.Addresses.Commands.UpdateAddress
{
    public sealed class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.CountryId).NotEmpty();
            RuleFor(x => x.CityId).NotEmpty();
            RuleFor(x => x.PostalCode).MaximumLength(20);
            RuleFor(x => x.BuildingNumber).MaximumLength(20);
            RuleFor(x => x.Street).MaximumLength(20);
            RuleFor(x => x.Description).MaximumLength(200);
        }
    }
}

