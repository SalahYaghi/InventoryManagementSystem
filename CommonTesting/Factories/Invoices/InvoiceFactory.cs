using Domain.Invoices;

namespace InventoryManagement.Tests.Common.Factories.Invoices;

public static class InvoiceFactory
{
    public static Invoice CreateValid(
        Guid? id = null,
        InvoiceType invoiceType = InvoiceType.Sale,
        decimal taxAmount = 0m,
        decimal discountAmount = 0m,
        List<InvoiceLineItem>? lineItems = null,
        Guid? orderId = null)
    {
        var result = Invoice.Create(
            id ?? Guid.NewGuid(),
            invoiceType,
             discountAmount,
            lineItems ?? new List<InvoiceLineItem> { InvoiceLineItemFactory.CreateValid() },
            orderId ?? Guid.NewGuid());
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }
}
