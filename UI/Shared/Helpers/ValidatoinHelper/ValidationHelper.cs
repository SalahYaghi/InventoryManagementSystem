 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Common.Helpers
{
    public class ValidationHelper
    {

        private static readonly Regex UsernameRegex = new Regex($@"^[A-Za-z][A-Za-z_0-9]{{{9//UserRules.UsernameMinLength 
            - 1},{9//UserRules.UsernameMaxLength
            - 1}}}$"
            , RegexOptions.Compiled);
        private static readonly Regex PasswordRegex = new Regex(@"^(?=.{8,})(?:(?=.*[a-z])(?=.*[A-Z])(?=.*\d)|(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9])|(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9])).*$"
            , RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(
             @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
            , RegexOptions.Compiled);

        public static bool ValidateEmail(string email){

            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }

        public static bool ValidatePhonenumber(string phonenumber) { 
        
            if(string.IsNullOrEmpty(phonenumber))
                return false;

            string pattern = @"^\+?[0-9]{7,15}$";
            return Regex.IsMatch(phonenumber, pattern, RegexOptions.IgnoreCase);
        }

        public static bool ValidatePassword(string password) =>
                PasswordRegex.IsMatch(password);

        public static bool ValidateUsername(string username) =>
              UsernameRegex.IsMatch(username);
        
        
        public static bool ValidateLocalUrl(string url)
        {

            if (string.IsNullOrEmpty(url))
                return false;

            return Uri.TryCreate(url , UriKind.Absolute , out var uriResult) || 
                Directory.Exists(url) || File.Exists(url);
        }

        public static bool ValidateHttpUrl(string url)
        {

            if (string.IsNullOrEmpty(url))
                return false;

            return Uri.TryCreate(url , UriKind.Absolute , out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps); 
        }
        public static bool IsValidImageUrlOrPath(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;

            value = value.Trim();

             if (Uri.TryCreate(value, UriKind.Absolute, out _))
                return true;

             if (Uri.TryCreate(value, UriKind.Relative, out _))
                return true;

           //  if (Path.IsP(value))
             //    return true;

            return false;
        }
        public static bool IsValidNationalNo(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                return false;

            if(!IsInRange(nationalNo.Length, 5, 20))
                return false;

            return !nationalNo.Any(s => !char.IsDigit(s));
        }

        public static bool IsInRange(int value, int min, int max, bool excludeUpper = false)
        {
            if (excludeUpper)
                return value >= min && value < max;

            return value >= min && value <= max;
        }

    }
}

