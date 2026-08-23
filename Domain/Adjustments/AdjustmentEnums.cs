namespace Domain.Adjustments
{
    public enum AdjustmentType
    {
        Increase = 1,
        Decrease = 2
    }

    public enum AdjustmentStatus
    {
        Draft = 1,
        Approved = 2,
        Cancelled = 3
    }

    public enum AdjustmentReason
    {
        Damaged = 1,
        Expired = 2,
        Lost = 3,
        CountDifference = 4,
        Other = 5, ExtraFound = 6
    }
}

