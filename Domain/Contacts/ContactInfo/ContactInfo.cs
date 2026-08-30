using Domain.Common.Errors;
using Domain.Common.Helpers;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Domain.Contacts.ContactInfo
{
    public class ContactInfo : AuditableEntity
    {

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? AlternitavePhoneNumber { get; set; } = string.Empty;
        public string ? FaxNumber { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; } = string.Empty;

        private ContactInfo() { }
        private ContactInfo(Guid id, string Email , 
         string PhoneNumber ,
         string? AlternitavePhoneNumber ,
         string? FaxNumber,
         string? WebsiteUrl) : base(id){
        
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.FaxNumber = FaxNumber;
            this.AlternitavePhoneNumber = AlternitavePhoneNumber;
            this.WebsiteUrl = WebsiteUrl;
    
        }
        public static Result<ContactInfo> Create(Guid id , string email , string phoneNumber , string alternitavePhoneNumber,  string ? faxNumber , string? websiteUrl) {

            if (!ValidationHelper.ValidateEmail(email))
                return ValidationErrors.EmailInvalid;
            if (!ValidationHelper.ValidatePhonenumber(phoneNumber))
                return ValidationErrors.PhoneNumberInvalid;
            if (!string.IsNullOrEmpty(alternitavePhoneNumber) 
                && !ValidationHelper.ValidatePhonenumber(alternitavePhoneNumber))
                return ValidationErrors.PhoneNumberInvalid;
            if (!string.IsNullOrEmpty(websiteUrl)
                && !ValidationHelper.ValidateUrl(websiteUrl))
                return ValidationErrors.UrlInvalid;
             
            
            return new ContactInfo(id , email , phoneNumber , alternitavePhoneNumber , faxNumber , websiteUrl); 
        }
        

        public Result<Updated> Update(
            string email,
            string phoneNumber,
            string? alternitavePhoneNumber,
            string? faxNumber,
            string? websiteUrl)
        {
            if (!ValidationHelper.ValidateEmail(email))
                return ValidationErrors.EmailInvalid;
            if (!ValidationHelper.ValidatePhonenumber(phoneNumber))
                return ValidationErrors.PhoneNumberInvalid;
            if (!string.IsNullOrEmpty(alternitavePhoneNumber)
                && !ValidationHelper.ValidatePhonenumber(alternitavePhoneNumber))
                return ValidationErrors.PhoneNumberInvalid;
            if (!string.IsNullOrEmpty(websiteUrl)
                && !ValidationHelper.ValidateUrl(websiteUrl))
                return ValidationErrors.UrlInvalid;
             
            Email = email;
            PhoneNumber = phoneNumber;
            AlternitavePhoneNumber = alternitavePhoneNumber;
            FaxNumber = faxNumber;
            WebsiteUrl = websiteUrl;

            return Result.Updated;
        }

    }
}

