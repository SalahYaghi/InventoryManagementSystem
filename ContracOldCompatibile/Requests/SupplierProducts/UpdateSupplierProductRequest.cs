using System.ComponentModel.DataAnnotations;
namespace Contract.Requests.SupplierProducts
{
public class UpdateSupplierProductRequest
{
    public bool IsActive { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "PurchasePrice must be greater than or equal to 0.")]
    public decimal PurchasePrice { get; set; }
}
}


