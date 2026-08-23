using System;
using System.ComponentModel.DataAnnotations;
namespace Contract.Requests.SupplierProducts
{
public class CreateSupplierProductRequest
{
 

    [Required(ErrorMessage = "ProductId is required.")]
    public Guid ProductId { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "PurchasePrice must be greater than or equal to 0.")]
    public decimal PurchasePrice { get; set; }
}
}

