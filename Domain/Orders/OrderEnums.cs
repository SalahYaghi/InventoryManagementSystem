namespace Domain.Orders
{
    public enum OrderType
    {
        Purchase = 1,
        Sale = 2,
        Transfer = 3 , 
        ReturnIn = 4 , // By Customer 
        ReturnOut = 5  // To Supplier
    }

    public enum OrderStatus
    {
        Pending = 1, // pending 
        Completed = 2, // due date is now , when tafsert from pending to compleete only 
        Cancelled = 3
    }
}

