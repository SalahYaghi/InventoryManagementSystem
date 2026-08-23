using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.ContactInfos.DTOs;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;

namespace Contract.Features.Parties.Customers.DTOs
{
    public sealed record CustomerDto
    {
        public Guid Id { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public string CustomerCode { get; init; } = string.Empty;
        public Guid ContactId { get; init; }
        public Guid AddressId { get; init; }
        public AddressDto? Address { get; init; }
        public ContactInfoDto? Contact { get; init; }

         public string? Notes { get; init; }
    }
    public class CustomerForListDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;

        public Guid ContactId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string BuildingNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public Guid AddressId { get; set; }

     }
}

