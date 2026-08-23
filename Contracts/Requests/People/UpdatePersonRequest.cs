using System;
using System.ComponentModel.DataAnnotations;
using Contract.Requests.Addresses;
using Contract.Requests.ContactInfos;
namespace Contract.Requests.People
{
public class UpdatePersonRequest
{
    [Required(ErrorMessage = "NationalNo is required.")]
    [MaxLength(20, ErrorMessage = "NationalNo must not exceed 20 characters.")]
    public string NationalNo { get; set; } = string.Empty;

    [Required(ErrorMessage = "FirstName is required.")]
    [MaxLength(10, ErrorMessage = "FirstName must not exceed 10 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "SecondName is required.")]
    [MaxLength(10, ErrorMessage = "SecondName must not exceed 10 characters.")]
    public string SecondName { get; set; } = string.Empty;

    [MaxLength(10, ErrorMessage = "ThirdName must not exceed 10 characters.")]
    public string ThirdName { get; set; }

    [Required(ErrorMessage = "LastName is required.")]
    [MaxLength(10, ErrorMessage = "LastName must not exceed 10 characters.")]
    public string LastName { get; set; } = string.Empty;

    public bool Gender { get; set; }

    public DateOnly DateOfBirth { get; set; }

   // [ValidateComplexType]
    public UpdateContactInfoRequest Contact { get; set; }

   // [ValidateComplexType]
    public UpdateAddressRequest Address { get; set; }
}
}

