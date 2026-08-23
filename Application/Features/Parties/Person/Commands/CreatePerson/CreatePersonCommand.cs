using Contract.Features.Parties.People.DTOs;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.Documents.Commands.CreateDocument;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System.Reflection.Metadata;

namespace Contract.Features.Parties.People.Commands.CreatePerson
{
    public sealed record CreatePersonCommand : IRequest<Result<PersonDto>>
    {
        public string NationalNo { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string SecondName { get; init; } = string.Empty;
        public string? ThirdName { get; init; }
        public string LastName { get; init; } = string.Empty;
        public bool Gender { get; init; }
        public DateOnly DateOfBirth { get; init; }
        public CreateContactInfoCommand Contact { get; init; } = default!;
        public CreateAddressCommand Address { get; init; } = default!;
    }
}

