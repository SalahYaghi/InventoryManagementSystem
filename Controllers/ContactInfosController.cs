using Domain.Common.Constants;
using Application.Common.Constants;
 using Application.Features.References.ContactInfos.Commands.CreateContactInfo;
using Application.Features.References.ContactInfos.Commands.DeleteContactInfo;
using Application.Features.References.ContactInfos.Commands.UpdateContactInfo;
using Application.Features.References.ContactInfos.DTOs;
using Application.Features.References.ContactInfos.Queries.GetContactInfo;
using Application.Features.References.ContactInfos.Queries.GetContactInfoPaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Contracts.Requests.ContactInfos;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/contact-infos")]
[ApiVersion("1.0")]
[Authorize]
public sealed class ContactInfosController(ISender sender) : ApiController
{
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.ContactInfo])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged contactinfos.")]
    [EndpointDescription("Returns a paginated list of contactinfos.")]
    [EndpointName("GetContactInfos")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetContactInfoPagedQuery { PageNumber = pageNumber, PageSize = pageSize }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{contactInfoId:guid}", Name = "GetContactInfoById")]
    [OutputCache(Tags = [CacheEntities.ContactInfo])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a contactinfo by ID.")]
    [EndpointDescription("Returns detailed information about the specified contactinfo.")]
    [EndpointName("GetContactInfoById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid contactInfoId, CancellationToken ct)
    {
        var result = await sender.Send(new GetContactInfoQuery(contactInfoId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new contactinfo.")]
    [EndpointDescription("Adds a new contactinfo to the system.")]
    [EndpointName("CreateContactInfo")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateContactInfoEntryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateContactInfoCommand
        {
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            AlternitavePhoneNumber = request.AlternitavePhoneNumber,
            FaxNumber = request.FaxNumber,
            WebsiteUrl = request.WebsiteUrl
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetContactInfoById", new { version = "1.0", contactInfoId = response.Id }, response),
            Problem);
    }

    [HttpPut("{contactInfoId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a contactinfo.")]
    [EndpointDescription("Updates the specified contactinfo.")]
    [EndpointName("UpdateContactInfo")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(Guid contactInfoId, [FromBody] UpdateContactInfoEntryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateContactInfoCommand
        {
            Id = contactInfoId,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            AlternitavePhoneNumber = request.AlternitavePhoneNumber,
            FaxNumber = request.FaxNumber,
            WebsiteUrl = request.WebsiteUrl
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{contactInfoId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a contactinfo.")]
    [EndpointDescription("Deletes the specified contactinfo.")]
    [EndpointName("DeleteContactInfo")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid contactInfoId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteContactInfoCommand(contactInfoId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
