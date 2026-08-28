using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Domain.Common.Helpers
{
    public static class ValidationHelper
    {
        public const int UsernameMinLength = 5;
        public const int UsernameMaxLength = 50;
        public const int PasswordMinLength = 8;
        public const int NationalNoMinLength = 5;
        public const int NationalNoMaxLength = 20;

        private static readonly Regex UsernameRegex = new Regex(
            @"^[A-Za-z][A-Za-z_0-9]{" + (UsernameMinLength - 1) + "," + (UsernameMaxLength - 1) + "}$",
            RegexOptions.Compiled);

        private static readonly Regex PasswordRegex = new Regex(
            @"^(?=.{8,})(?:(?=.*[a-z])(?=.*[A-Z])(?=.*\d)|(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9])|(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z0-9])|(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9])).*$",
            RegexOptions.Compiled);

        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled);

        private static readonly Regex PhoneRegex = new Regex(
            @"^\+?[0-9]{7,15}$",
            RegexOptions.Compiled);

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email.Trim());
        }

        public static bool ValidatePhonenumber(string phonenumber)
        {
            if (string.IsNullOrWhiteSpace(phonenumber))
                return false;

            return PhoneRegex.IsMatch(phonenumber.Trim());
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return PasswordRegex.IsMatch(password);
        }

        public static string DescribePasswordRules()
        {
            return "Password must be at least " + PasswordMinLength +
                   " characters and combine at least three of: lowercase letter, uppercase letter, digit, symbol.";
        }

        public static bool ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            return UsernameRegex.IsMatch(username.Trim());
        }

        public static string DescribeUsernameRules()
        {
            return "Username must be " + UsernameMinLength + " to " + UsernameMaxLength +
                   " characters, start with a letter and contain only letters, digits or underscores.";
        }

        public static bool ValidateLocalUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            Uri uriResult;

            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) ||
                   Directory.Exists(url) ||
                   File.Exists(url);
        }

        public static bool ValidateHttpUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            Uri uriResult;

            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static bool IsValidImageUrlOrPath(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;

            value = value.Trim();

            if (value.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                return false;

            Uri parsed;

            if (Uri.TryCreate(value, UriKind.Absolute, out parsed))
                return true;

            return Uri.TryCreate(value, UriKind.Relative, out parsed);
        }

        public static bool IsValidNationalNo(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                return false;

            nationalNo = nationalNo.Trim();

            if (!IsInRange(nationalNo.Length, NationalNoMinLength, NationalNoMaxLength))
                return false;

            return nationalNo.All(char.IsDigit);
        }

        public static bool IsInRange(int value, int min, int max, bool excludeUpper = false)
        {
            if (excludeUpper)
                return value >= min && value < max;

            return value >= min && value <= max;
        }
    }
}
