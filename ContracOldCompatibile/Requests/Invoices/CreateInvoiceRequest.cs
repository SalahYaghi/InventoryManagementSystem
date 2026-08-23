using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Requests.Invoices
{
    public class CreateInvoiceRequest
    {
        public Guid OrderId { get; set; }

    }
}



