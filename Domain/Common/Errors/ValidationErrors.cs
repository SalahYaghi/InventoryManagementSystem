using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Errors
{
    public class ValidationErrors
    {
        public static Error EmailInvalid = Error.Validation(
            "Email.Invalid",
            "The email address is not in a valid format."
        );

        public static Error PhoneNumberInvalid = Error.Validation(
            "PhoneNumber.Invalid",
            "The phone number is not valid."
        ); public static Error UrlInvalid = Error.Validation(
    "Url.Invalid",
    "The website URL is not valid."
);

        public static Error FaxInvalid = Error.Validation(
            "Fax.Invalid",
            "The fax number is not valid."
        );
    }
}

