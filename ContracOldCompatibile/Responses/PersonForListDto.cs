using System;
namespace Contract.Responses
{
    public class PersonForListDto
    {
        public Guid Id { get; set; }
        public string NationalNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid ?DocumentId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}



