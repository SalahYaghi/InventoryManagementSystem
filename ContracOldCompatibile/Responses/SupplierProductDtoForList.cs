using System;
namespace Contract.Responses
{
    public class SupplierProductDtoForList
    {
        public string ProductName { get; set; } = string.Empty;
         public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid Id { get; set; }
        public decimal PurchasePrice { get; set; }
         public byte[] RowVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}



