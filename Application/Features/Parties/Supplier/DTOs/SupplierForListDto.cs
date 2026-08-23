using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Supplier.DTOs
{
    public class SupplierForListDto
    {
        public Guid Id { get; init; }
        public string SupplierName { get; init; } = string.Empty;
        public string SupplierCode { get; init; } = string.Empty;
  
        public Guid ContactId { get; init; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string? BuildingNumber { get; set; } = string.Empty;
        public string? Street { get; set; } = string.Empty;
        public Guid AddressId { get; init; }
        
        public bool Status { get; init; }
    }
}

