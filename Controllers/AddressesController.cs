using Domain.Common.Constants;
using Application.Common.Constants;
 using Application.Features.References.Addresses.Commands.CreateAddress;
using Application.Features.References.Addresses.Commands.DeleteAddress;
using Application.Features.References.Addresses.Commands.UpdateAddress;
using Application.Features.References.Addresses.DTOs;
using Application.Features.References.Addresses.Queries.GetAddress;
using Application.Features.References.Addresses.Queries.GetAddressPaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Contracts.Requests.Addresses;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/addresses")]
[ApiVersion("1.0")]
[Authorize]
public sealed class AddressesController(ISender sender) : ApiController
{
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Address])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged addresses.")]
    [EndpointDescription("Returns a paginated list of addresses.")]
    [EndpointName("GetAddresses")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetAddressPagedQuery { PageNumber = pageNumber, PageSize = pageSize }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{addressId:guid}", Name = "GetAddressById")]
    [OutputCache(Tags = [CacheEntities.Address])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves an address by ID.")]
    [EndpointDescription("Returns detailed information about the specified address.")]
    [EndpointName("GetAddressById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid addressId, CancellationToken ct)
    {
        var result = await sender.Send(new GetAddressQuery(addressId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new address.")]
    [EndpointDescription("Adds a new address to the system.")]
    [EndpointName("CreateAddress")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateAddressEntryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateAddressCommand
        {
            CountryId = request.CountryId,
            CityId = request.CityId,
            PostalCode = request.PostalCode,
            BuildingNumber = request.BuildingNumber,
            Street = request.Street,
            Description = request.Description
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetAddressById", new { version = "1.0", addressId = response.Id }, response),
            Problem);
    }

    [HttpPut("{addressId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates an address.")]
    [EndpointDescription("Updates the specified address.")]
    [EndpointName("UpdateAddress")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(Guid addressId, [FromBody] UpdateAddressEntryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateAddressCommand
        {
            Id = addressId,
            CountryId = request.CountryId,
            CityId = request.CityId,
            PostalCode = request.PostalCode,
            BuildingNumber = request.BuildingNumber,
            Street = request.Street,
            Description = request.Description
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{addressId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes an address.")]
    [EndpointDescription("Deletes the specified address.")]
    [EndpointName("DeleteAddress")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid addressId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteAddressCommand(addressId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
