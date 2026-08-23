using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Transactions.Invoice.DTOs
{
    public class InvoiceLineItemDto
    {
        public int LineNo { get;  set; }
        public string Description { get;  set; } = string.Empty;
        public decimal Quantity { get;  set; }
        public decimal UnitPrice { get;  set; }
        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }//=> UnitPrice * Quantity;

    }
}

