using Contract.Features.Parties.People.DTOs;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Domain.Contacts.ContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.Documents.Commands.UpdateDocument;

namespace Contract.Features.Parties.People.Commands.UpdatePerson
{
    public sealed record UpdatePersonCommand : IRequest<Result<PersonDto>>
    {
        public Guid Id { get; init; }
        public string NationalNo { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string SecondName { get; init; } = string.Empty;
        public string? ThirdName { get; init; }
        public string LastName { get; init; } = string.Empty;
        public bool Gender { get; init; }
        public DateOnly DateOfBirth { get; init; }
        public UpdateContactInfoCommand? Contact { get; init; }
        public UpdateAddressCommand? Address { get; init; }
    }
}

