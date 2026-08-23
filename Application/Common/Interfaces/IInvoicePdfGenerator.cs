using Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface IInvoicePdfGenerator
    {
        byte[] Generate(Invoice invoice); 
    }
}

