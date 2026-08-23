namespace Contract.Responses
{
    public class InvoiceLineItemDto
    {
        public int LineNo { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount => UnitPrice * Quantity;
    }
}



