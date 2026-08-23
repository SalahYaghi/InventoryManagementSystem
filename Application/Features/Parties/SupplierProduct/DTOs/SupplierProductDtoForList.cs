using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.SupplierProduct.DTOs
{
    public class SupplierProductDtoForList
    {
        public string ProductName { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid Id { get; set; }
        public decimal PurchasePrice { get; set; }  
        public bool    IsActive { get; set; }
        public byte[] RowVersion { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

    }

}

