using Domain.Common.Constants;
using








    Contract.Common.Constants;
 using Contract.Features.Inventory.Categories.Commands.CreateCategory;
using Contract.Features.Inventory.Categories.Commands.DeleteCategory;
using Contract.Features.Inventory.Categories.Commands.UpdateCategory;
using Contract.Features.Inventory.Categories.DTOs;
using Contract.Features.Inventory.Categories.Queries.GetCategory;
using Contract.Features.Inventory.Categories.Queries.GetCategoryPaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Contract.Requests.Categories;
using Microsoft.AspNetCore.Http;
using Infrastructure.Policies.OutputCachePolicies;
using Contract.Requests.Categories;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/categories")]
[ApiVersion("1.0")]
[Authorize]
public sealed class CategoriesController(ISender sender) : ApiController
{
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Category], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByQueryKeys = ["pageNumber", "pageSize"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged categories.")]
    [EndpointDescription("Returns a paginated list of categories.")]
    [EndpointName("GetCategories")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetCategoryPagedQuery
        {
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{categoryId:guid}", Name = "GetCategoryById")]
    [OutputCache(Tags = [CacheEntities.Category], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["categoryId"])]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a category by ID.")]
    [EndpointDescription("Returns detailed information about the specified category.")]
    [EndpointName("GetCategoryById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid categoryId, CancellationToken ct)
    {
        var result = await sender.Send(new GetCategoryQuery(categoryId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new category.")]
    [EndpointDescription("Adds a new category to the system.")]
    [EndpointName("CreateCategory")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateCategoryCommand
        {
            Name = request.Name
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetCategoryById", new { version = "1.0", categoryId = response.Id }, response),
            Problem);
    }

    [HttpPut("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a category.")]
    [EndpointDescription("Updates the specified category.")]
    [EndpointName("UpdateCategory")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(Guid categoryId, [FromBody] UpdateCategoryRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateCategoryCommand
        {
            Id = categoryId,
            Name = request.Name
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a category.")]
    [EndpointDescription("Deletes the specified category.")]
    [EndpointName("DeleteCategory")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid categoryId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteCategoryCommand(categoryId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
