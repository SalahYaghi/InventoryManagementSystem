using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Customer
{
    public class Customer : AuditableEntity
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public Guid ContactId { get; private set; }
        public ContactInfo? Contact { get; private set; }

        public Guid AddressId { get; private set; }
        public Address? Address { get; private set; }

        public string? Notes { get; set; }

        private Customer() { }

        private Customer(
            Guid id,
            string customerName,
            string customerCode,
               ContactInfo? contact,
            Address? address,
            string? notes) : base(id)
        {
            CustomerName = customerName;
            CustomerCode = customerCode;
            Contact = contact;
            Address = address;
            Notes = notes;
        }

        public static Result<Customer> Create(
            Guid id,
            string customerName,
            string customerCode,
           ContactInfo? contact,
            Address? address,
            string? notes)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return CustomerErrors.NameRequired;

            if (customerName.Length > 50)
                return CustomerErrors.NameTooLong;

            if (string.IsNullOrWhiteSpace(customerCode))
                return CustomerErrors.CodeRequired;

            if (customerCode.Length > 50)
                return CustomerErrors.CodeTooLong;

            if (contact is null)
                return CustomerErrors.ContactRequired;

            if (address is null)
                return CustomerErrors.AddressRequired;

            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return CustomerErrors.NotesTooLong;

            var customer = new Customer(
                id,
                customerName,
                customerCode,
                contact,
                address,
                notes
            );

            return customer;
        }

        public Result<Updated> Update(
            string customerName,
            string customerCode,
            ContactInfo? contact,
            Address? address,
            string? notes)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return CustomerErrors.NameRequired;

            if (customerName.Length > 50)
                return CustomerErrors.NameTooLong;

            if (string.IsNullOrWhiteSpace(customerCode))
                return CustomerErrors.CodeRequired;

            if (customerCode.Length > 50)
                return CustomerErrors.CodeTooLong;
             
            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return CustomerErrors.NotesTooLong;

            CustomerName = customerName;
            CustomerCode = customerCode;
          
            Notes = notes;
            if (address is not null)
            {

                if (this.Address == null)
                    this.Address = address;
                else
                {

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
            return Result.Updated;
        }

    }
}
