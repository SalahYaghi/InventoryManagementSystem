using System;
namespace Contract.Responses
{
    public class SupplierProductDto
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool IsActive { get; set; }
    }
}



