using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contacts.Address
{
    public class AddressErrors
    {
        public static Error CountryRequired = Error.Validation(
            "Address.CountryRequired",
            "Country is required."
        );

        public static Error CityRequired = Error.Validation(
            "Address.CityRequired",
            "City is required."
        );

        public static Error PostalCodeInvalid = Error.Validation(
            "Address.PostalCodeInvalid",
            "Postal code is not valid."
        );

        public static Error BuildingNumberInvalid = Error.Validation(
            "Address.BuildingNumberInvalid",
            "Building number is not valid."
        );

        public static Error StreetInvalid = Error.Validation(
            "Address.StreetInvalid",
            "Street is not valid."
        );

        public static Error DescriptionTooLong = Error.Validation(
            "Address.DescriptionTooLong",
            "Description exceeds the maximum allowed length."
        );
    }
}

