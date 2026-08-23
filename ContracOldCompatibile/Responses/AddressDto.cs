using System;
namespace Contract.Responses
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public string PostalCode { get; set; }
        public string BuildingNumber { get; set; }
        public string Street { get; set; }
        public string Description { get; set; }
        public CityDto City { get; set; }
        public CountryDto Country { get; set; }
    }
}



