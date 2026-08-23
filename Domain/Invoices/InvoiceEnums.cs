namespace Domain.Invoices
{
    public enum InvoiceType
    {
        Purchase = 1,
        Sale = 2 , 
        ReturnIn= 3 , 
        ReturnOut = 4
    }

    public enum InvoiceStatus
    {
        Issued = 2,
        Paid = 3,
        Refunded = 5 ,
    }
}

