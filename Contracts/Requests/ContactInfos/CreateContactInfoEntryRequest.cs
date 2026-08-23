using System.ComponentModel.DataAnnotations;
namespace Contract.Requests.ContactInfos
{
public class CreateContactInfoEntryRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    [MaxLength(256, ErrorMessage = "Email must not exceed 256 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "PhoneNumber is required.")]
    [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Phone number must be 7–15 digits and may start with '+'.")]
    [MaxLength(20, ErrorMessage = "PhoneNumber must not exceed 20 characters.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(20, ErrorMessage = "AlternitavePhoneNumber must not exceed 20 characters.")]
    public string AlternitavePhoneNumber { get; set; } = string.Empty;

    [MaxLength(20, ErrorMessage = "FaxNumber must not exceed 20 characters.")]
    public string FaxNumber { get; set; }

    [MaxLength(500, ErrorMessage = "WebsiteUrl must not exceed 500 characters.")]
    public string WebsiteUrl { get; set; }
}
}

