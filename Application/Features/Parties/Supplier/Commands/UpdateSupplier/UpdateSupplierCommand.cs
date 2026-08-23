using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using MechanicShop.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Parties.Supplier.Commands.UpdateSupplier
{
    public sealed record UpdateSupplierCommand : IRequest<Result<SupplierDto>>
    {
        public Guid Id { get; init; }
        public string SupplierName { get; init; } = string.Empty;
        public string SupplierCode { get; init; } = string.Empty;
        public UpdateContactInfoCommand? Contact { get; init; } = default!;
        public UpdateAddressCommand? Address { get; init; } = default!;
        public bool Status { get; init; }
        public string? Notes { get; init; }
    }
}

