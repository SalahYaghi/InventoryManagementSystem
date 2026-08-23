using System.ComponentModel.DataAnnotations;
using Contract.Requests.Addresses;
using Contract.Requests.ContactInfos;
namespace Contract.Requests.Customers
{
public class CreateCustomerRequest
{
    [Required(ErrorMessage = "CustomerName is required.")]
    [MaxLength(50, ErrorMessage = "CustomerName must not exceed 50 characters.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "CustomerCode is required.")]
    [MaxLength(50, ErrorMessage = "CustomerCode must not exceed 50 characters.")]
    public string CustomerCode { get; set; } = string.Empty;

  //  [ValidateComplexType]
    public CreateContactInfoRequest Contact { get; set; } = new();

    [Required(ErrorMessage = "Address is required.")]
  //  [ValidateComplexType]
    public CreateAddressRequest Address { get; set; } = new();

 
    [MaxLength(500, ErrorMessage = "Notes must not exceed 500 characters.")]
    public string Notes { get; set; }
}
}

