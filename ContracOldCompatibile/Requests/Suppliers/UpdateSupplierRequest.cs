using System.ComponentModel.DataAnnotations;
using Contract.Requests.Addresses;
using Contract.Requests.ContactInfos;
namespace Contract.Requests.Suppliers
{
public class UpdateSupplierRequest
{
    [Required(ErrorMessage = "SupplierName is required.")]
    [MaxLength(50, ErrorMessage = "SupplierName must not exceed 50 characters.")]
    public string SupplierName { get; set; } = string.Empty;

    [Required(ErrorMessage = "SupplierCode is required.")]
    [MaxLength(50, ErrorMessage = "SupplierCode must not exceed 50 characters.")]
    public string SupplierCode { get; set; } = string.Empty;

    //[ValidateComplexType]
    public UpdateContactInfoRequest Contact { get; set; }

    //[ValidateComplexType]
    public UpdateAddressRequest Address { get; set; }

    public bool Status { get; set; }

    [MaxLength(500, ErrorMessage = "Notes must not exceed 500 characters.")]
    public string Notes { get; set; }
}
}


