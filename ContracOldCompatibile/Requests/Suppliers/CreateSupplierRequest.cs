using System.ComponentModel.DataAnnotations;
using Contract.Requests.Addresses;
using Contract.Requests.ContactInfos;
namespace Contract.Requests.Suppliers
{
public class CreateSupplierRequest
{
    [Required(ErrorMessage = "SupplierName is required.")]
    [MaxLength(50, ErrorMessage = "SupplierName must not exceed 50 characters.")]
    public string SupplierName { get; set; } = string.Empty;

    [Required(ErrorMessage = "SupplierCode is required.")]
    [MaxLength(50, ErrorMessage = "SupplierCode must not exceed 50 characters.")]
    public string SupplierCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact is required.")]
   // [ValidateComplexType]
    public CreateContactInfoRequest Contact { get; set; } = new CreateContactInfoRequest();

    [Required(ErrorMessage = "Address is required.")]
 //   [ValidateComplexType]
    public CreateAddressRequest Address { get; set; } = new CreateAddressRequest();

    public bool Status { get; set; }

    [MaxLength(500, ErrorMessage = "Notes must not exceed 500 characters.")]
    public string Notes { get; set; }
}
}


