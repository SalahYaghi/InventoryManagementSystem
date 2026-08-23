using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Person.DTOs
{
    public class PersonForListDto
    {
        public Guid Id { get; init; }
        public string NationalNo { get; init; } = string.Empty;
        public string FullName { get; init; } = string.Empty;
        public string Gender { get; init; }
        public Guid? DocumentId { get; set; }

        public DateOnly DateOfBirth { get; init; }
        public string PhoneNumber { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;

    }
}

