using Domain.Contacts.Address;
using Domain.Contacts.ContactInfo;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Suppliers
{
    public class Supplier : AuditableEntity
    {
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierCode { get; set; } = string.Empty;
        public Guid ContactId { get; private set; }
        public ContactInfo? Contact { get; private set; }

        public Guid AddressId { get; private set; }
        public Address? Address { get; private set; }
        public bool Status { get; set; }
        public string? Notes { get; set; }

        private Supplier() { }

        private Supplier(
            Guid id,
            string supplierName,
            string supplierCode,
            ContactInfo contact,
            Address address,
            bool status,
            string? notes) : base(id)
        {
            SupplierName = supplierName;
            SupplierCode = supplierCode;
            Contact = contact;
            Address = address;
            Status = status;
            Notes = notes;
        }

        public static Result<Supplier> Create(
            Guid id,
            string supplierName,
            string supplierCode,
            ContactInfo? contact,
            Address? address,
            bool status,
            string? notes)
        {
            if (string.IsNullOrWhiteSpace(supplierName))
                return SupplierErrors.NameRequired;

            if (supplierName.Length > 50)
                return SupplierErrors.NameTooLong;

            if (string.IsNullOrWhiteSpace(supplierCode))
                return SupplierErrors.CodeRequired;

            if (supplierCode.Length > 50)
                return SupplierErrors.CodeTooLong;

            if (contact is null)
                return SupplierErrors.ContactRequired;

            if (address is null)
                return SupplierErrors.AddressRequired;

            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return SupplierErrors.NotesTooLong;

            var supplier = new Supplier(
                id,
                supplierName,
                supplierCode,
                contact,
                address,
                status,
                notes
            );

            return supplier;
        
        }
        public Result<Updated> Update(
            string supplierName,
            string supplierCode,
          ContactInfo? contact,
            Address? address, bool status,
            string? notes)
        {
            if (string.IsNullOrWhiteSpace(supplierName))
                return SupplierErrors.NameRequired;

            if (supplierName.Length > 50)
                return SupplierErrors.NameTooLong;

            if (string.IsNullOrWhiteSpace(supplierCode))
                return SupplierErrors.CodeRequired;

            if (supplierCode.Length > 50)
                return SupplierErrors.CodeTooLong;

            
            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return SupplierErrors.NotesTooLong;

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
            SupplierName = supplierName;
            SupplierCode = supplierCode;
            Status = status;
            Notes = notes;

            return Result.Updated;
        }

    } }
