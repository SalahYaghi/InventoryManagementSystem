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

using Contract.Features.Parties.Supplier.Queries.GetSupplier;

namespace SubcutaneousTests.Features.Parties.Supplier.Queries.GetSupplier;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetSupplierQueryHandlerTests
{
    private readonly IMediator _mediator;
    private readonly IAppDbContext _context;

    public GetSupplierQueryHandlerTests(WebAppFactory factory)
    {
        _context = factory.CreateAppDbContext();
        _mediator = factory.CreateMediator();
    }

    [Fact]
    public async Task Handle_WithMissingSupplier_ShouldFail()
    {
        var result = await _mediator.Send(new GetSupplierQuery(Guid.NewGuid()));
        Assert.True(result.IsError);
    }
}
