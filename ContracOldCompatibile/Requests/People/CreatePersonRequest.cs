using System;
using System.ComponentModel.DataAnnotations;
using Contract.Requests.Addresses;
using Contract.Requests.ContactInfos;
namespace Contract.Requests.People
{
public class CreatePersonRequest
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

    public DateTimeOffset DateOfBirth { get; set; }

    [Required(ErrorMessage = "Contact is required.")]
    //[ValidateComplexType]
    public CreateContactInfoRequest Contact { get; set; } = new CreateContactInfoRequest();

    [Required(ErrorMessage = "Address is required.")]
 //   [ValidateComplexType]
    public CreateAddressRequest Address { get; set; } = new CreateAddressRequest();
}
}


