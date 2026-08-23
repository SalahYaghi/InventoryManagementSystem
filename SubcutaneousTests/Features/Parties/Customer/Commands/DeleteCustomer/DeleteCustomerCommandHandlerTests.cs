using Contract.Common.Interfaces;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using InventoryManagement.Tests.Common.Factories.Contacts;
using InventoryManagement.Tests.Common.Factories.Customers;
using InventoryManagement.Tests.Common.Factories.Identity;
using InventoryManagement.Tests.Common.Factories.People;
using InventoryManagement.Tests.Common.Factories.Products;
using InventoryManagement.Tests.Common.Factories.Suppliers;
using InventoryManagement.Tests.Common.Factories.Warehouses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SubcutaneousTests.Common;
using Xunit;
using Xunit.Abstractions;

using Contract.Features.Parties.Customers.Commands.DeleteCustomer;

namespace SubcutaneousTests.Features.Parties.Customer.Commands.DeleteCustomer;

[Collection(WebAppFactoryCollection.CollectionName)]
public class DeleteCustomerCommandHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public DeleteCustomerCommandHandlerTests(WebAppFactory factory)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
    }

    [Fact]
    public async Task Handle_WithMissingCustomer_ShouldFail()
    {
        var result = await _mediator.Send(new DeleteCustomerCommand(Guid.NewGuid()));
        Assert.True(result.IsError);
    }
}
