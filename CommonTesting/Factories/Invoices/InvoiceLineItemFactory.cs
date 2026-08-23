using Domain.Invoices;

namespace InventoryManagement.Tests.Common.Factories.Invoices;

public static class InvoiceLineItemFactory
{
    public static InvoiceLineItem CreateValid(
        int lineNo = 1,
        Guid? invoiceId = null,
        string name = "Invoice item",
        decimal tax = 0m,
        decimal quantity = 2m,
        decimal unitPrice = 10m)
    {
        var result = InvoiceLineItem.Create(lineNo, invoiceId ?? Guid.NewGuid(), name, tax, quantity, unitPrice);
        if (result.IsError) throw new InvalidOperationException(result.TopError.Description);
        return result.Value;
    }
}
