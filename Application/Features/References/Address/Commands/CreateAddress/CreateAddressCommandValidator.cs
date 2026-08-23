using FluentValidation;

namespace Contract.Features.References.Addresses.Commands.CreateAddress
{
    public sealed class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
             RuleFor(x => x.CountryId).NotEqual(Guid.Empty);
            RuleFor(x => x.CityId).NotEqual(Guid.Empty);
            RuleFor(x => x.PostalCode).MaximumLength(20);
            RuleFor(x => x.BuildingNumber).MaximumLength(20);
            RuleFor(x => x.Street).MaximumLength(20);
            RuleFor(x => x.Description).MaximumLength(200);
        }
    }
}

