using Domain.Common.Constants;
using Application.Common.Constants;
using Application.Features.References.Cities.Queries.GetCityPaged;
 using Application.Features.References.Countries.Commands.CreateCountry;
using Application.Features.References.Countries.Commands.DeleteCountry;
using Application.Features.References.Countries.Commands.UpdateCountry;
using Application.Features.References.Countries.DTOs;
using Application.Features.References.Countries.Queries.GetCountry;
using Application.Features.References.Countries.Queries.GetCountryPaged;
using Contracts.Requests.Countries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/countries")]
[ApiVersion("1.0")]
[Authorize]
public sealed class CountriesController(ISender sender) : ApiController
{
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Country])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    // [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    [EndpointSummary("Retrieves paged countries.")]
    [EndpointDescription("Returns a paginated list of countries.")]
    [EndpointName("GetCountries")]
    [MapToApiVersion("1.0")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(CancellationToken ct = default)
    {
        var result = await sender.Send(new GetCountryPagedQuery(), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{countryId:guid}/cities")]
    [OutputCache(Tags = [CacheEntities.City])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    // [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    [EndpointSummary("Retrieves paged countries.")]
    [EndpointDescription("Returns a paginated list of countries.")]
    [EndpointName("GetCountries")]
    [MapToApiVersion("1.0")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCities(Guid countryId, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetCityByCountryIdPagedQuery(countryId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{countryId:guid}", Name = "GetCountryById")]
    [OutputCache(Tags = [CacheEntities.Country])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    // [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    [EndpointSummary("Retrieves a country by ID.")]
    [EndpointDescription("Returns detailed information about the specified country.")]
    [EndpointName("GetCountryById")]
    [MapToApiVersion("1.0")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid countryId, CancellationToken ct)
    {
        var result = await sender.Send(new GetCountryQuery(countryId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new country.")]
    [EndpointDescription("Adds a new country to the system.")]
    [EndpointName("CreateCountry")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateCountryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateCountryCommand { Id = request.Id, Name = request.Name }, ct);
        return result.Match(
            response => CreatedAtRoute("GetCountryById", new { version = "1.0", countryId = response.Id }, response),
            Problem);
    }

    [HttpPut("{countryId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a country.")]
    [EndpointDescription("Updates the specified country.")]
    [EndpointName("UpdateCountry")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(Guid countryId, [FromBody] UpdateCountryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateCountryCommand { Id = countryId, Name = request.Name }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{countryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a country.")]
    [EndpointDescription("Deletes the specified country.")]
    [EndpointName("DeleteCountry")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid countryId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteCountryCommand(countryId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
