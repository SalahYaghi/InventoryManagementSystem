using Domain.Invoices;
using Xunit;

namespace InventoryManagement.Application.DomainTesting.Invoices;

public class InvoiceTests
{
    private static InvoiceLineItem Line(decimal quantity = 2m, decimal unitPrice = 50m, decimal tax = 5m)
        => InvoiceLineItem.Create(1, Guid.NewGuid(), "Item", tax, quantity, unitPrice).Value;

    [Fact]
    public void Create_WithValidData_SucceedsWithIssuedStatus()
    {
        var result = Invoice.Create(
            Guid.NewGuid(), InvoiceType.Sale, 10m,
            new List<InvoiceLineItem> { Line() }, Guid.NewGuid());

        Assert.True(result.IsSuccess);
        Assert.Equal(InvoiceStatus.Issued, result.Value.Status);
        Assert.Equal(InvoiceType.Sale, result.Value.InvoiceType);
    }

    [Fact]
    public void Create_WithUndefinedInvoiceType_Fails()
    {
        var result = Invoice.Create(
            Guid.NewGuid(), (InvoiceType)99, 0m,
            new List<InvoiceLineItem> { Line() }, Guid.NewGuid());

        Assert.Equal(InvoiceErrors.InvalidInvoiceType.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNegativeDiscount_Fails()
    {
        var result = Invoice.Create(
            Guid.NewGuid(), InvoiceType.Sale, -1m,
            new List<InvoiceLineItem> { Line() }, Guid.NewGuid());

        Assert.Equal(InvoiceErrors.DiscountAmountInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNoLineItems_Fails()
    {
        var result = Invoice.Create(
            Guid.NewGuid(), InvoiceType.Sale, 0m,
            new List<InvoiceLineItem>(), Guid.NewGuid());

        Assert.Equal(InvoiceErrors.InvoiceLineItemsRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Amounts_AreComputedFromLineItems()
    {
        // line1: 2 * 50 = 100, tax 5 ; line2: 1 * 30 = 30, tax 3
        var invoice = Invoice.Create(
            Guid.NewGuid(), InvoiceType.Sale, 10m,
            new List<InvoiceLineItem>
            {
                Line(quantity: 2m, unitPrice: 50m, tax: 5m),
                InvoiceLineItem.Create(2, Guid.NewGuid(), "Second", 3m, 1m, 30m).Value
            },
            Guid.NewGuid()).Value;

        Assert.Equal(130m, invoice.SubTotalAmount);
        Assert.Equal(8m, invoice.TaxAmount);
        Assert.Equal(128m, invoice.NetAmount); // 130 + 8 - 10
    }

    [Fact]
    public void Create_WithEmptyOrderId_ShouldFail()
    {
        var result = Invoice.Create(
            Guid.NewGuid(), InvoiceType.Sale, 0m,
            new List<InvoiceLineItem> { Line() }, Guid.Empty);

        Assert.True(result.IsError); // succeeds today
    }


    [Fact]
    public void Create_WithDiscountExceedingTotal_ShouldFail()
    {
        var result = Invoice.Create(
            Guid.NewGuid(), InvoiceType.Sale, 1000m,
            new List<InvoiceLineItem> { Line(quantity: 1m, unitPrice: 10m, tax: 0m) },
            Guid.NewGuid());

        Assert.True(result.IsError || result.Value.NetAmount >= 0);
     }

}

public class InvoiceLineItemTests
{
    [Fact]
    public void Create_WithValidData_MapsAllFieldsCorrectly()
    {
        
        var result = InvoiceLineItem.Create(
            lineNo: 7, invoiceId: Guid.NewGuid(), name: "Widget",
            tax: 3m, quantity: 4m, unitPrice: 25m);

        Assert.True(result.IsSuccess);
        Assert.Equal(7, result.Value.LineNo);
        Assert.Equal(4m, result.Value.Quantity);
        Assert.Equal(3m, result.Value.Tax);
        Assert.Equal(25m, result.Value.UnitPrice);
        Assert.Equal(100m, result.Value.TotalAmount);
    }

    [Fact]
    public void Create_WithNegativeLineNo_Fails()
    {
        var result = InvoiceLineItem.Create(-1, Guid.NewGuid(), "X", 0m, 1m, 1m);
        Assert.Equal(LineItemErrors.LineNoInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithEmptyInvoiceId_Fails()
    {
        var result = InvoiceLineItem.Create(1, Guid.Empty, "X", 0m, 1m, 1m);
        Assert.Equal(LineItemErrors.InvoiceRequired.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Create_WithMissingName_Fails(string? name)
    {
        var result = InvoiceLineItem.Create(1, Guid.NewGuid(), name!, 0m, 1m, 1m);
        Assert.Equal(LineItemErrors.NameRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNameOver100Chars_Fails()
    {
        var result = InvoiceLineItem.Create(1, Guid.NewGuid(), new string('N', 101), 0m, 1m, 1m);
        Assert.Equal(LineItemErrors.NameTooLong.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void Create_WithNonPositiveQuantity_Fails(decimal quantity)
    {
        var result = InvoiceLineItem.Create(1, Guid.NewGuid(), "X", 0m, quantity, 1m);
        Assert.Equal(LineItemErrors.QuantityInvalid.Code, result.TopError.Code);
    }

    [Fact]
    public void Create_WithNegativeUnitPrice_Fails()
    {
        var result = InvoiceLineItem.Create(1, Guid.NewGuid(), "X", 0m, 1m, -1m);
        Assert.Equal(LineItemErrors.UnitPriceInvalid.Code, result.TopError.Code);
    }

    [Fact]
    [Trait("Category", "BugExposing")]
    public void Create_WithNegativeTax_ShouldFail()
    {
        var result = InvoiceLineItem.Create(1, Guid.NewGuid(), "X", -10m, 1m, 5m);

        Assert.True(result.IsError); 
    }

    [Fact]
    public void Update_WithValidData_ChangesFields()
    {
        var item = InvoiceLineItem.Create(1, Guid.NewGuid(), "Old", 1m, 1m, 10m).Value;
        var newInvoiceId = Guid.NewGuid();

        var result = item.Update(newInvoiceId, "New", 3m, 20m);

        Assert.True(result.IsSuccess);
        Assert.Equal("New", item.Description);
        Assert.Equal(3m, item.Quantity);
        Assert.Equal(20m, item.UnitPrice);
        Assert.Equal(60m, item.TotalAmount);
    }

    [Fact]
    public void Update_WithInvalidData_DoesNotMutate()
    {
        var item = InvoiceLineItem.Create(1, Guid.NewGuid(), "Old", 1m, 1m, 10m).Value;

        var result = item.Update(Guid.NewGuid(), "New", 0m, 20m); // quantity 0 invalid

        Assert.True(result.IsError);
        Assert.Equal("Old", item.Description);
        Assert.Equal(1m, item.Quantity);
    }
}
