using System;
namespace Contract.Responses
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierCode { get; set; } = string.Empty;
        public Guid ContactId { get; set; }
        public ContactInfoDto Contact { get; set; }

        public Guid AddressId { get; set; }
        public AddressDto Address { get; set; }

        public bool Status { get; set; }
        public string Notes { get; set; }
    }
    public class SupplierForListDto
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierCode { get; set; } = string.Empty;

        public Guid ContactId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string BuildingNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public Guid AddressId { get; set; }

        public bool Status { get; set; }
    }
}



