using Domain.Common.Constants;
using Microsoft.AspNetCore.Routing;

using Contract.Common.Constants;
using Contract.Features.References.Cities.Commands.CreateCity;
using Contract.Features.References.Cities.Commands.DeleteCity;
using Contract.Features.References.Cities.Commands.UpdateCity;
using Contract.Features.References.Cities.DTOs;
using Contract.Features.References.Cities.Queries.GetCity;
using Contract.Features.References.Cities.Queries.GetCityPaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Contract.Requests.Cities;
using Microsoft.AspNetCore.Http;
using Infrastructure.Policies.OutputCachePolicies;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/cities")]
[ApiVersion("1.0")]
[Authorize]
public sealed class CitiesController(ISender sender) : ApiController
{
    [HttpGet("{cityId:guid}", Name = "GetCityById")]
    [OutputCache(Tags = [CacheEntities.City], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["cityId"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    [EndpointSummary("Retrieves a city by ID.")]
    [EndpointDescription("Returns detailed information about the specified city.")]
    [EndpointName("GetCityById")]
    [MapToApiVersion("1.0")]
     
    public async Task<IActionResult> GetById(Guid cityId, CancellationToken ct)
    {
        var result = await sender.Send(new GetCityQuery(cityId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new city.")]
    [EndpointDescription("Adds a new city to the system.")]
    [EndpointName("CreateCity")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateCityRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateCityCommand { Id = request.Id, Name = request.Name }, ct);
        return result.Match(
            response => CreatedAtRoute("GetCityById", new { version = "1.0", cityId = response.Id }, response),
            Problem);
    }

    [HttpPut("{cityId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a city.")]
    [EndpointDescription("Updates the specified city.")]
    [EndpointName("UpdateCity")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(Guid cityId, [FromBody] UpdateCityRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateCityCommand { Id = cityId, Name = request.Name }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{cityId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a city.")]
    [EndpointDescription("Deletes the specified city.")]
    [EndpointName("DeleteCity")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid cityId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteCityCommand(cityId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
