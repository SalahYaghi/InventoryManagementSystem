using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.ContactInfos.DTOs;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;

namespace Contract.Features.Parties.Supplier.DTOs
{
    public sealed record SupplierDto
    {
        public Guid Id { get; init; }
        public string SupplierName { get; init; } = string.Empty;
        public string SupplierCode { get; init; } = string.Empty;
      
        
        public Guid ContactId { get; init; }
        public ContactInfoDto? Contact { get; init; }

        public Guid AddressId { get; init; }
        public AddressDto? Address { get; init; }
   
        public bool Status { get; init; }
        public string? Notes { get; init; }
    }
}

