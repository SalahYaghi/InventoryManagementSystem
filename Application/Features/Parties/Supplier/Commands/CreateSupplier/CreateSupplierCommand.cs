using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Inventory.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Parties.Supplier.Commands.CreateSupplier
{
    public sealed record CreateSupplierCommand : IRequest<Result<SupplierDto>>
    {
        public Guid Id { get; init; }
        public string SupplierName { get; init; } = string.Empty;
        public string SupplierCode { get; init; } = string.Empty;
        public CreateContactInfoCommand Contact { get; init; } = default!;
        public CreateAddressCommand Address { get; init; } = default!;
        public bool Status { get; init; }
        public string? Notes { get; init; }
    }
}

