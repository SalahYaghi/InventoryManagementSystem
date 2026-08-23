using Domain.Common.Constants;
using Application.Common.Constants;
 using Application.Features.Parties.Customers.Commands.CreateCustomer;
using Application.Features.Parties.Customers.Commands.DeleteCustomer;
using Application.Features.Parties.Customers.Commands.UpdateCustomer;
using Application.Features.Parties.Customers.DTOs;
using Application.Features.Parties.Customers.Queries.GetCustomer;
using Application.Features.Parties.Customers.Queries.GetCustomerPaged;
using Application.Features.References.Addresses.Commands.CreateAddress;
using Application.Features.References.Addresses.Commands.UpdateAddress;
using Application.Features.References.ContactInfos.Commands.CreateContactInfo;
using Application.Features.References.ContactInfos.Commands.UpdateContactInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Contracts.Requests.Customers;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/customers")]
[ApiVersion("1.0")]
[Authorize]
public sealed class CustomersController(ISender sender) : ApiController
{
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Customer])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged customers.")]
    [EndpointDescription("Returns a paginated list of customers.")]
    [EndpointName("GetCustomers")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get(  CancellationToken ct = default)
    {
        var result = await sender.Send(new Application.Features.Parties.Customers.Queries.GetCustomerPaged.GetCustomerQuery {  }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{customerId:guid}", Name = "GetCustomerById")]
    [OutputCache(Tags = [CacheEntities.Customer])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a customer by ID.")]
    [EndpointDescription("Returns detailed information about the specified customer.")]
    [EndpointName("GetCustomerById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid customerId, CancellationToken ct)
    {
        var result = await sender.Send(new Application.Features.Parties.Customers.Queries.GetCustomer.GetCustomerQuery(customerId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new customer.")]
    [EndpointDescription("Adds a new customer to the system.")]
    [EndpointName("CreateCustomer")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateCustomerCommand
        {
            CustomerName = request.CustomerName,
            CustomerCode = request.CustomerCode,
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
             Notes = request.Notes
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetCustomerById", new { version = "1.0", customerId = response.Id }, response),
            Problem);
    }

    [HttpPut("{customerId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a customer.")]
    [EndpointDescription("Updates the specified customer.")]
    [EndpointName("UpdateCustomer")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser)]
    public async Task<IActionResult> Update(Guid customerId, [FromBody] UpdateCustomerRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateCustomerCommand
        {
            Id = customerId,
            CustomerName = request.CustomerName,
            CustomerCode = request.CustomerCode,
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
             Notes = request.Notes
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a customer.")]
    [EndpointDescription("Deletes the specified customer.")]
    [EndpointName("DeleteCustomer")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid customerId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteCustomerCommand(customerId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
