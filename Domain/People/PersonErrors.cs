using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.People
{
    public static class PersonErrors
    {
        public static readonly Error NationalNoRequired = Error.Validation(
            "Person.NationalNoRequired",
            "National number is required.");

        public static readonly Error NationalNoInvalid = Error.Validation(
            "Person.NationalNoInvalid",
            "National number is invalid.");

        public static readonly Error FirstNameRequired = Error.Validation(
            "Person.FirstNameRequired",
            "First name is required.");

        public static readonly Error FirstNameTooLong = Error.Validation(
            "Person.FirstNameTooLong",
            "First name exceeds the maximum allowed length.");

        public static readonly Error SecondNameRequired = Error.Validation(
            "Person.SecondNameRequired",
            "Second name is required.");

        public static readonly Error SecondNameTooLong = Error.Validation(
            "Person.SecondNameTooLong",
            "Second name exceeds the maximum allowed length.");

        public static readonly Error ThirdNameTooLong = Error.Validation(
            "Person.ThirdNameTooLong",
            "Third name exceeds the maximum allowed length.");

        public static readonly Error LastNameRequired = Error.Validation(
            "Person.LastNameRequired",
            "Last name is required.");

        public static readonly Error LastNameTooLong = Error.Validation(
            "Person.LastNameTooLong",
            "Last name exceeds the maximum allowed length.");

        public static readonly Error DateOfBirthInvalid = Error.Validation(
            "Person.DateOfBirthInvalid",
            "Date of birth is invalid.");

        public static readonly Error ImageUrlInvalid = Error.Validation(
            "Person.ImageUrlInvalid",
            "Image URL is invalid.");

        public static readonly Error ContactRequired = Error.Validation(
            "Person.ContactRequired",
            "Contact is required.");

        public static readonly Error AddressRequired = Error.Validation(
            "Person.AddressRequired",
            "Address is required.");

        public static readonly Error DocumentRequired = Error.Validation(
            "Person.DocumentRequired",
            "Document is required.");
    }
}

