using Domain.Common.Helpers;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Common.Helpers;

public class ValidationHelperTests
{
    // ---------------- Email ----------------

    [Theory]
    [InlineData("user@example.com", true)]
    [InlineData("a@b.co", true)]
    [InlineData("first.last@sub.domain.org", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("no-at-sign.com", false)]
    [InlineData("two@@example.com", false)]
    [InlineData("user@nodot", false)]
    [InlineData("user @example.com", false)]
    public void ValidateEmail_Cases(string email, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.ValidateEmail(email));
    }

    [Fact]
    public void ValidateEmail_Null_ReturnsFalse()
    {
        Assert.False(ValidationHelper.ValidateEmail(null!));
    }

    // ---------------- Phone ----------------

    [Theory]
    [InlineData("+972590000000", true)]
    [InlineData("0591234567", true)]
    [InlineData("1234567", true)]        // 7 digits = min
    [InlineData("123456789012345", true)] // 15 digits = max
    [InlineData("123456", false)]         // 6 digits, too short
    [InlineData("1234567890123456", false)] // 16 digits, too long
    [InlineData("059-123-4567", false)]   // dashes not allowed by the regex
    [InlineData("", false)]
    [InlineData("abcdefg", false)]
    public void ValidatePhonenumber_Cases(string phone, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.ValidatePhonenumber(phone));
    }

    // ---------------- Username (rules: 5-20 chars, starts with letter, letters/digits/underscore) ----------------

    [Theory]
    [InlineData("abcde", true)]                  // min length 5
    [InlineData("abcd", false)]                  // 4 chars, too short
    [InlineData("a1234", true)]
    [InlineData("user_name_20_chars_x", true)]   // exactly 20
    [InlineData("user_name_21_chars_xx", false)] // 21, too long
    [InlineData("1abcd", false)]                 // must start with a letter
    [InlineData("_abcd", false)]                 // must start with a letter
    [InlineData("ab cd", false)]                 // no spaces
    [InlineData("ab-cd", false)]                 // no dashes
    public void ValidateUsername_Cases(string username, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.ValidateUsername(username));
    }

    [Fact]
    public void ValidateUsername_Null_ShouldReturnFalse_NotThrow()
    {
        var result = ValidationHelper.ValidateUsername(null!); 
        Assert.False(result);
    }


    [Theory]
    [InlineData("Abcdef12", true)]    // upper + lower + digit
    [InlineData("Abcdefg!", true)]    // upper + lower + special
    [InlineData("abcdef1!", true)]    // lower + digit + special
    [InlineData("ABCDEF1!", true)]    // upper + digit + special
    [InlineData("abcdefgh", false)]   // one category only
    [InlineData("Abcdefgh", false)]   // two categories only
    [InlineData("Ab1!", false)]       // too short
    [InlineData("", false)]
    public void ValidatePassword_Cases(string password, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.ValidatePassword(password));
    }

    [Fact]
    public void ValidatePassword_Null_ShouldReturnFalse_NotThrow()
    {
        var result = ValidationHelper.ValidatePassword(null!); // throws today
        Assert.False(result);
    }

    // NOTE (not a test): UserRules.PasswordMaxLength = 16, but the password
    // regex enforces no maximum. Decide which is right and align them.

    // ---------------- Url ----------------

    [Theory]
    [InlineData("https://example.com", true)]
    [InlineData("http://example.com/page?x=1", true)]
    [InlineData("ftp://example.com", false)]     // only http/https allowed
    [InlineData("example.com", false)]           // not absolute
    [InlineData("", false)]
    public void ValidateUrl_Cases(string url, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.ValidateUrl(url));
    }

    // ---------------- IsValidImageUrlOrPath ----------------

    [Theory]
    [InlineData(null, true)]     // null/empty are explicitly allowed
    [InlineData("", true)]
    [InlineData("https://cdn.example.com/img.png", true)]
    [InlineData(@"C:\images\product.png", true)]
    public void IsValidImageUrlOrPath_AcceptedCases(string? value, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.IsValidImageUrlOrPath(value));
    }

    // ⚠ BUG-EXPOSING TEST — expected to FAIL until the domain is fixed.
    //
    // The relative-Uri branch `Uri.TryCreate(value, UriKind.Relative, out _)`
    // succeeds for virtually ANY string ("hello world", "!!!", "<script>"...),
    // which makes this validation a no-op: nothing is ever rejected.
    // Either remove the relative branch or validate the string against an
    // allowed pattern (extension whitelist, no invalid path chars, etc.).
    [Theory]
    [Trait("Category", "BugExposing")]
    [InlineData("hello world this is not a path or url")]
    [InlineData("!!!???")]
    public void IsValidImageUrlOrPath_ShouldRejectGarbage(string value)
    {
        Assert.False(ValidationHelper.IsValidImageUrlOrPath(value)); // returns true today
    }

    // ---------------- NationalNo ----------------

    [Theory]
    [InlineData("12345", true)]                 // min 5
    [InlineData("12345678901234567890", true)]  // max 20
    [InlineData("1234", false)]                 // too short
    [InlineData("123456789012345678901", false)] // 21, too long
    [InlineData("12a45", false)]                // non-digit
    [InlineData("", false)]
    [InlineData("   ", false)]
    public void IsValidNationalNo_Cases(string nationalNo, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.IsValidNationalNo(nationalNo));
    }

    // ---------------- IsInRange ----------------

    [Theory]
    [InlineData(5, 1, 10, false, true)]
    [InlineData(1, 1, 10, false, true)]   // inclusive lower
    [InlineData(10, 1, 10, false, true)]  // inclusive upper
    [InlineData(10, 1, 10, true, false)]  // exclusive upper
    [InlineData(0, 1, 10, false, false)]
    [InlineData(11, 1, 10, false, false)]
    public void IsInRange_Cases(int value, int min, int max, bool excludeUpper, bool expected)
    {
        Assert.Equal(expected, ValidationHelper.IsInRange(value, min, max, excludeUpper));
    }
}
