using System;
using System.ComponentModel.DataAnnotations;
 namespace Contract.Requests.Products
{
public class UpdateProductRequest
{
    [Required(ErrorMessage = "SKU is required.")]
    [MaxLength(10, ErrorMessage = "SKU must not exceed 10 characters.")]
    public string SKU { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "BarCode must not exceed 50 characters.")]
    public string BarCode { get; set; }

    [Required(ErrorMessage = "ProductName is required.")]
    [MaxLength(30, ErrorMessage = "ProductName must not exceed 30 characters.")]
    public string ProductName { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "SellingPrice must be greater than or equal to 0.")]
    public decimal SellingPrice { get; set; }

    public bool IsActive { get; set; }

    public Unit Unit { get; set; }

    [Required(ErrorMessage = "CategoryId is required.")]
    public Guid CategoryId { get; set; }
}
}


