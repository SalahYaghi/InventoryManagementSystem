using System;
namespace Contract.Responses
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public Guid ContactId { get; set; }
        public Guid AddressId { get; set; }
         public ContactInfoDto? Contact { get; set; }

         public AddressDto? Address { get; set; }

        public string? Notes { get; set; }
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


