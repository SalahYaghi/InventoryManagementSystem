using Contract.Features.Transactions.Invoice.Commands.CreateInvoice;
using FluentValidation.TestHelper;
using Xunit;

namespace SubcutaneousTests.Features.Transactions.Invoice.Commands.CreateInvoice;

public class CreateInvoiceCommandValidatorTests
{
    private readonly CreateInvoiceCommandValidator _validator = new();

    [Fact]
    public async Task Validate_WithValidOrderId_ShouldNotHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new CreateInvoiceCommand { OrderId = Guid.NewGuid() });
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validate_WithEmptyOrderId_ShouldHaveValidationError()
    {
        var result = await _validator.TestValidateAsync(new CreateInvoiceCommand { OrderId = Guid.Empty });
        result.ShouldHaveValidationErrorFor(x => x.OrderId);
    }
}
