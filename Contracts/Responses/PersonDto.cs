using System;
namespace Contract.Responses
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string NationalNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string ThirdName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public Guid ContactId { get; set; }
        public Guid AddressId { get; set; }
        public Guid DocumentId { get; set; }
        public ContactInfoDto Contact { get; set; } = new();
        public DocumentDto Document { get; set; } = new();
        public AddressDto Address { get; set; } = new();
    }
}


