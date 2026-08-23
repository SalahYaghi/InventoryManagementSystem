using Domain.Common.Constants;
using Application.Common.Constants;
using Application.Features.Inventory.Warehouses.Commands.CreateWarehouse;
using Application.Features.Inventory.Warehouses.Commands.DeleteWarehouse;
using Application.Features.Inventory.Warehouses.Commands.UpdateWarehouse;
using Application.Features.Inventory.Warehouses.DTOs;
using Application.Features.Inventory.Warehouses.Queries.GetWarehouse;
using Application.Features.Inventory.Warehouses.Queries.GetWarehousePaged;
using Contracts.Requests.Warehouses;
using Domain.Contacts.Address;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;


namespace InventoryManagementSystemAPI.Controllers
{
 

    [Route("api/v{version:apiVersion}/warehouses")]
    [ApiVersion("1.0")]
    [Authorize]
    public sealed class WarehousesController(ISender sender) : ApiController
    {
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Warehouse])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged warehouses.")]
    [EndpointDescription("Returns a paginated list of warehouses.")]
    [EndpointName("GetWarehouses")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get( CancellationToken ct = default)
        {
            var result = await sender.Send(new GetWarehousesQuery
            {
            }, ct);

            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("{warehouseId:guid}", Name = "GetWarehouseById")]
    [OutputCache(Tags = [CacheEntities.Warehouse])]
    [ProducesResponseType(typeof(WarehouseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a warehous by ID.")]
    [EndpointDescription("Returns detailed information about the specified warehous.")]
    [EndpointName("GetWarehousById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid warehouseId, CancellationToken ct)
        {
            var result = await sender.Send(new GetWarehouseQuery(warehouseId), ct);
            return result.Match(response => Ok(response), Problem);
        }
    [HttpPost]
    [ProducesResponseType(typeof(WarehouseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new warehous.")]
    [EndpointDescription("Adds a new warehous to the system.")]
    [EndpointName("CreateWarehouse")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> Create([FromBody] CreateWarehouseRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new CreateWarehouseCommand
            {
                Name = request.Name,
                Code = request.Code,
                Address = new Application.Features.References.Addresses.Commands.CreateAddress.CreateAddressCommand()
                {
                    BuildingNumber  = request.Address.BuildingNumber,
                    CityId = request.Address.CityId,
                    CountryId = request.Address.CountryId,
                    Description = request.Address.Description,
                    PostalCode = request.Address.PostalCode,
                    Street  = request.Address.Street
                }
            }, ct);

            return result.Match(
                response => CreatedAtRoute("GetWarehouseById", new { version = "1.0", warehouseId = response.Id }, response),
                Problem);
        }
    [HttpPut("{warehouseId:guid}")]
    [ProducesResponseType(typeof(WarehouseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a warehous.")]
    [EndpointDescription("Updates the specified warehous.")]
    [EndpointName("UpdateWarehouse")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> Update(Guid warehouseId, [FromBody] UpdateWarehouseRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateWarehouseCommand
            {
                Id = warehouseId,
                Name = request.Name,
   
                Code = request.Code,
                Address =  request.Address == null ? null :  new Application.Features.References.Addresses.Commands.UpdateAddress.UpdateAddressCommand()
                {
                    BuildingNumber = request.Address.BuildingNumber,
                    CityId = request.Address.CityId,
                    CountryId = request.Address.CountryId,
                    Description = request.Address.Description,
                    PostalCode = request.Address.PostalCode,
                    Street = request.Address.Street
                },
                WarehouseStatus = (Domain.Warehouses.WarehouseStatus)request.WarehouseStatus
            }, ct);

            return result.Match(response => Ok(response), Problem);
        }
    [HttpDelete("{warehouseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a warehous.")]
    [EndpointDescription("Deletes the specified warehous.")]
    [EndpointName("DeleteWarehouse")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid warehouseId, CancellationToken ct)
        {
            var result = await sender.Send(new DeleteWarehouseCommand(warehouseId), ct);
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
