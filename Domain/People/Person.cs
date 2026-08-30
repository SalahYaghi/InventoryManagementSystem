using Domain.Common.Helpers;
using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using Domain.Document;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;

namespace Domain.People
{
    public class Person : AuditableEntity
    {

        public string FullName => FirstName + " " + SecondName + " " + (string.IsNullOrWhiteSpace(ThirdName) ? "" : ThirdName + " ") + LastName;
        public string NationalNo { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string? ThirdName { get; private set; }
        public string LastName { get; private set; }
        public bool Gender { get; private set; }
        public DateOnly DateOfBirth { get; private set; }

        public string? ImageUrl { get; private set; }

        public Guid ContactId { get; private set; }
        public ContactInfo? Contact { get; private set; }

        public Guid AddressId { get; private set; }
        public Address? Address { get; private set; }

        public Document.Document? Document { get; private set; }
        public Guid ? DocumentId { get; private set; }

        private Person() { }
        private Person(
            Guid id,
            string nationalNo,
            string firstName,
            string secondName,
            string? thirdName,
            string lastName,
            bool gender,
            DateOnly dateOfBirth,
            ContactInfo? contact,
            Address? address) : base(id)
        {
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Contact = contact;
            Address = address;
        }

        public static Result<Person> Create(
          Guid id,
            string nationalNo,
            string firstName,
            string secondName,
            string? thirdName,
            string lastName,
            bool gender,
            DateOnly dateOfBirth,
            ContactInfo? contact,
            Address? address)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                return PersonErrors.NationalNoRequired;

            if (!ValidationHelper.IsValidNationalNo(nationalNo))
                return PersonErrors.NationalNoInvalid;

            if (string.IsNullOrWhiteSpace(firstName))
                return PersonErrors.FirstNameRequired;

            if (firstName.Length > 10)
                return PersonErrors.FirstNameTooLong;

            if (string.IsNullOrWhiteSpace(secondName))
                return PersonErrors.SecondNameRequired;

            if (secondName.Length > 10)
                return PersonErrors.SecondNameTooLong;

            if (!string.IsNullOrWhiteSpace(thirdName) && thirdName.Length > 10)
                return PersonErrors.ThirdNameTooLong;

            if (string.IsNullOrWhiteSpace(lastName))
                return PersonErrors.LastNameRequired;

            if (lastName.Length > 10)
                return PersonErrors.LastNameTooLong;

            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Today))
                return PersonErrors.DateOfBirthInvalid;


            if (contact == default)
                return PersonErrors.ContactRequired;

            if (address == default)
                return PersonErrors.AddressRequired;

            var person = new Person(
                id,
                nationalNo,
                firstName,
                secondName,
                thirdName,
                lastName,
                gender,
                dateOfBirth,
                contact,
                address

            );

            return person;
        }


        public Result<Updated> Update(
            string nationalNo,
            string firstName,
            string secondName,
            string? thirdName,
            string lastName,
            bool gender,
            DateOnly dateOfBirth,
            ContactInfo? contact,
            Address? address)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                return PersonErrors.NationalNoRequired;

            if (!ValidationHelper.IsValidNationalNo(nationalNo))
                return PersonErrors.NationalNoInvalid;

            if (string.IsNullOrWhiteSpace(firstName))
                return PersonErrors.FirstNameRequired;

            if (firstName.Length > 10)
                return PersonErrors.FirstNameTooLong;

            if (string.IsNullOrWhiteSpace(secondName))
                return PersonErrors.SecondNameRequired;

            if (secondName.Length > 10)
                return PersonErrors.SecondNameTooLong;

            if (!string.IsNullOrWhiteSpace(thirdName) && thirdName.Length > 10)
                return PersonErrors.ThirdNameTooLong;

            if (string.IsNullOrWhiteSpace(lastName))
                return PersonErrors.LastNameRequired;

            if (lastName.Length > 10)
                return PersonErrors.LastNameTooLong;

            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Today))
                return PersonErrors.DateOfBirthInvalid;

             
            if (address is not null) {

                if (this.Address == null)
                    this.Address = address;
                else {

                    var updateResult = this.Address.Update(
                        address.CountryId,
                        address.CityId,
                        address.PostalCode,
                        address.BuildingNumber,
                        address.Street,
                        address.Description
                    );

                    if (updateResult.IsError)
                    {
                        return updateResult.Errors;
                    }
                }

            }


            if (contact is not null)
            {

                if (this.Contact == null)
                    this.Contact = contact;
                else
                {
                    var updateResult = this.Contact.Update(
                        contact.Email,
                        contact.PhoneNumber,
                        contact.AlternitavePhoneNumber,
                        contact.FaxNumber,
                        contact.WebsiteUrl
                    );

                    if (updateResult.IsError)
                    {
                        return updateResult.Errors;
                    }

                }

            }


            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;

            return Result.Updated;
        }

        public Result<Updated> UpdateDocument(Document.Document document)
        {
            if (document == null)
                return PersonErrors.DocumentRequired;

            Document = document;
            DocumentId = document.Id;
            return Result.Updated;
        }

        public Result<Updated> UpdateImageUrl(string? imageUrl) { 
     
            this.ImageUrl = imageUrl;
            return Result.Updated;
        }

    }
} 

