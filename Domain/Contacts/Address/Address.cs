using Domain.Contacts.Address.Country;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contacts.Address
{
    public class Address : AuditableEntity
    {
        public Guid CountryId{ get; set; }
        public Country.Country? Country { get; set; }

        public Guid CityId { get; set; }
        public Country.City? City { get; set; }

        public string? PostalCode { get; set; }  = string.Empty;
        public string? BuildingNumber { get; set; } = string.Empty;
        public string? Street { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty; 


        private Address(Guid id , Guid countryId , Guid cityId , 
            string? postalCode , string? buildingNumber , string? street , string? description) : base(id) { 
        
            this.CountryId = countryId;
            this.CityId    = cityId;
            
            this.BuildingNumber = buildingNumber;
            this.Street = street;   
            this.PostalCode = postalCode;
            this.Description = description;
        
        }
        private Address() { }

        public static Result<Address> Create(
        Guid id,
        Guid countryId,
        Guid cityId,
        string? postalCode,
        string? buildingNumber,
        string? street,
        string? description)
        {

            if (countryId == Guid.Empty)
                return AddressErrors.CountryRequired;

            if (cityId == Guid.Empty)
                return AddressErrors.CityRequired;

            if (!string.IsNullOrWhiteSpace(postalCode) && postalCode.Length > 20)
                return AddressErrors.PostalCodeInvalid;

            if (!string.IsNullOrWhiteSpace(buildingNumber) && buildingNumber.Length > 20)
                return AddressErrors.BuildingNumberInvalid;


            if (!string.IsNullOrWhiteSpace(street) && street.Length > 20)
                return AddressErrors.StreetInvalid;

            if (!string.IsNullOrWhiteSpace(description) && description.Length > 200)
                return AddressErrors.DescriptionTooLong;

            var address = new Address(
                id,
                countryId,
                cityId,
                postalCode,
                buildingNumber,
                street,
                description
            );

            return (address);
        }

        public Result<Updated> Update(
            Guid countryId,
            Guid cityId,
            string? postalCode,
            string? buildingNumber,
            string? street,
            string? description)
        {
            if (countryId == Guid.Empty)
                return AddressErrors.CountryRequired;

            if (cityId == Guid.Empty)
                return AddressErrors.CityRequired;

            if (!string.IsNullOrWhiteSpace(postalCode) && postalCode.Length > 20)
                return AddressErrors.PostalCodeInvalid;

            if (!string.IsNullOrWhiteSpace(buildingNumber) && buildingNumber.Length > 20)
                return AddressErrors.BuildingNumberInvalid;

            if (!string.IsNullOrWhiteSpace(street) && street.Length > 20)
                return AddressErrors.StreetInvalid;

            if (!string.IsNullOrWhiteSpace(description) && description.Length > 200)
                return AddressErrors.DescriptionTooLong;

            CountryId = countryId;
            CityId = cityId;
            PostalCode = postalCode;
            BuildingNumber = buildingNumber;
            Street = street;
            Description = description;

            return Result.Updated;
        }

    }
}

