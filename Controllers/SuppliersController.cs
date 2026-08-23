using Domain.Common.Constants;
using Application.Common.Constants;
 using Application.Features.Parties.Supplier.Commands.CreateSupplier;
using Application.Features.Parties.Supplier.Commands.DeleteSupplier;
using Application.Features.Parties.Supplier.Commands.UpdateSupplier;
using Application.Features.Parties.Supplier.DTOs;
using Application.Features.Parties.Supplier.Queries.GetSupplier;
using Application.Features.Parties.Supplier.Queries.GetSupplierPaged;
using Application.Features.Parties.SupplierProducts.Commands.CreateSupplierProduct;
using Application.Features.Parties.SupplierProducts.Commands.DeleteSupplierProduct;
using Application.Features.Parties.SupplierProducts.Commands.UpdateSupplierProduct;
using Application.Features.Parties.SupplierProducts.Queries.GetSupplierProduct;
using Application.Features.Parties.SupplierProducts.Queries.GetSupplierProductPaged;
using Application.Features.References.Addresses.Commands.CreateAddress;
using Application.Features.References.Addresses.Commands.UpdateAddress;
using Application.Features.References.ContactInfos.Commands.CreateContactInfo;
using Application.Features.References.ContactInfos.Commands.UpdateContactInfo;
using Contracts.Requests.SupplierProducts;
using Contracts.Requests.Suppliers;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/suppliers")]
[ApiVersion("1.0")]
[Authorize]
public sealed class SuppliersController(ISender sender) : ApiController
{
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Supplier])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged suppliers.")]
    [EndpointDescription("Returns a paginated list of suppliers.")]
    [EndpointName("GetSuppliers")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get(CancellationToken ct = default)
    {
        var result = await sender.Send(new GetSupplierPagedQuery {   }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{supplierId:guid}", Name = "GetSupplierById")]
    [OutputCache(Tags = [CacheEntities.Supplier])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a supplier by ID.")]
    [EndpointDescription("Returns detailed information about the specified supplier.")]
    [EndpointName("GetSupplierById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid supplierId, CancellationToken ct)
    {
        var result = await sender.Send(new GetSupplierQuery(supplierId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new supplier.")]
    [EndpointDescription("Adds a new supplier to the system.")]
    [EndpointName("CreateSupplier")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser)]
    public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateSupplierCommand
        {
            Id = Guid.NewGuid(),
            SupplierName = request.SupplierName,
            SupplierCode = request.SupplierCode,
            Contact = new CreateContactInfoCommand
            {
                Email = request.Contact.Email,
                PhoneNumber = request.Contact.PhoneNumber,
                AlternitavePhoneNumber = request.Contact.AlternitavePhoneNumber,
                FaxNumber = request.Contact.FaxNumber,
                WebsiteUrl = request.Contact.WebsiteUrl
            },
            Address = new CreateAddressCommand
            {
                CountryId = request.Address.CountryId,
                CityId = request.Address.CityId,
                PostalCode = request.Address.PostalCode,
                BuildingNumber = request.Address.BuildingNumber,
                Street = request.Address.Street,
                Description = request.Address.Description
            },
            Status = request.Status,
            Notes = request.Notes
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetSupplierById", new { version = "1.0", supplierId = response.Id }, response),
            Problem);
    }

    [HttpPut("{supplierId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a supplier.")]
    [EndpointDescription("Updates the specified supplier.")]
    [EndpointName("UpdateSupplier")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser)]
    public async Task<IActionResult> Update(Guid supplierId, [FromBody] UpdateSupplierRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateSupplierCommand
        {
            Id = supplierId,
            SupplierName = request.SupplierName,
            SupplierCode = request.SupplierCode,
            Contact = request.Contact is null ? null : new UpdateContactInfoCommand
            {
                Email = request.Contact.Email,
                PhoneNumber = request.Contact.PhoneNumber,
                AlternitavePhoneNumber = request.Contact.AlternitavePhoneNumber,
                FaxNumber = request.Contact.FaxNumber,
                WebsiteUrl = request.Contact.WebsiteUrl
            },
            Address = request.Address is null ? null : new UpdateAddressCommand
            {
                CountryId = request.Address.CountryId,
                CityId = request.Address.CityId,
                PostalCode = request.Address.PostalCode,
                BuildingNumber = request.Address.BuildingNumber,
                Street = request.Address.Street,
                Description = request.Address.Description
            },
            Status = request.Status,
            Notes = request.Notes
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{supplierId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a supplier.")]
    [EndpointDescription("Deletes the specified supplier.")]
    [EndpointName("DeleteSupplier")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid supplierId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteSupplierCommand(supplierId), ct);
        return result.Match(_ => NoContent(), Problem);
    }

    [HttpGet("{supplierId:guid}/products")]
    [OutputCache(Tags = [CacheEntities.SupplierProduct])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged suppliers.")]
    [EndpointDescription("Returns a paginated list of suppliers.")]
    [EndpointName("GetSuppliers")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetAllSupplierProducts(Guid supplierId , CancellationToken ct) {

         var result =   await sender.Send(new GetSupplierProductsPagedQuery(supplierId));

        return result.Match(
            res => Ok(res) , 
            Problem
            );
    }

    [HttpPost("{supplierId:guid}/products")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new supplier.")]
    [EndpointDescription("Adds a new supplier to the system.")]
    [EndpointName("CreateSupplierProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser)]
    public async Task<IActionResult> CreateSupplierProduct(Guid supplierId , 
        CreateSupplierProductRequest request) {

        var result = await sender.Send(new CreateSupplierProductCommand()
        {
            SupplierId = supplierId,
            ProductId  = request.ProductId,
            PurchasePrice = request.PurchasePrice
        });

        return result.Match(res => CreatedAtRoute("GetSupplierProduct" , new { version = "1.0" ,
            supplierId = res.SupplierId,
            productId  = res.ProductId
        } , res), Problem);

    }

    [HttpPut("{supplierId:guid}/products/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a supplier.")]
    [EndpointDescription("Updates the specified supplier.")]
    [EndpointName("UpdateSupplierProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser)]
    public async Task<IActionResult> UpdateSupplierProduct(Guid supplierId,Guid productId,
        UpdateSupplierProductRequest request)
    {

        var result = await sender.Send(new UpdateSupplierProductCommand()
        {
            ProductId = productId,
            SupplierId = supplierId,
            IsActive = request.IsActive,
            PurchasePrice = request.PurchasePrice
        });

        return result.Match(_ => NoContent(), Problem);

    }

    [HttpDelete("{supplierId:guid}/products/{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a supplier.")]
    [EndpointDescription("Deletes the specified supplier.")]
    [EndpointName("DeleteSupplierProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteSupplierProduct(Guid supplierId, Guid productId)
    {

        var result = await sender.Send(new DeleteSupplierProductCommand(
             supplierId, productId));

        return result.Match(_ => NoContent(), Problem);

    }

    [HttpGet("{supplierId:guid}/products/{productId:guid}" , Name ="GetSupplierProduct")]
    [OutputCache(Tags = [CacheEntities.SupplierProduct])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a supplier by ID.")]
    [EndpointDescription("Returns detailed information about the specified supplier.")]
    [EndpointName("GetSupplierProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetSupplierProduct(Guid supplierId, Guid productId)
    {

        var result = await sender.Send(new GetSupplierProductQuery(
             supplierId, productId));

        return result.Match(res => Ok(res), Problem);

    }



}
