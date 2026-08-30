using Application.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById;
using Contract.Common.Constants;
using Contract.Features.Inventory.Product.Commands.CreateProduct;
using Contract.Features.Inventory.Product.Queries.GetProduct;
using Contract.Features.Inventory.WarehouseStock.Commands.AddWarehouseProducts;
using Contract.Features.Inventory.WarehouseStocks.Commands.DeleteWarehouseStock;
using Contract.Features.Inventory.WarehouseStocks.Commands.UpdateWarehouseStock;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged;
using Contract.Requests.Warehouses;
using Domain.Common.Constants;
using Infrastructure.Policies.OutputCachePolicies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
namespace InventoryManagementSystemAPI.Controllers
{
     


    [Route("api/v{version:apiVersion}/warehouse-stocks")]
    [ApiVersion("1.0")]
    [Authorize]
    public sealed class WarehouseStocksController(ISender sender) : ApiController
    {
    [HttpGet("warehouse/{warehouseId:guid}")]
     [OutputCache(Tags = [CacheEntities.WarehouseStock], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByQueryKeys = ["pageNumber", "pageSize"], VaryByRouteValueNames = ["warehouseId"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged warehousestocks.")]
    [EndpointDescription("Returns a paginated list of warehousestocks.")]
    [EndpointName("GetWarehouseStocks")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get(Guid warehouseId, [FromQuery] int pageNumber = 1
        , [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await sender.Send(new GetWarehouseStockPagedQuery(warehouseId)
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            }, ct);

            return result.Match(response => Ok(response), Problem);
        }



        [HttpGet("{warehouseStockId:guid}", Name = "GetWarehouseStockById")]
        [OutputCache(Tags = [CacheEntities.WarehouseStock], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["warehouseStockId"])]
        [ProducesResponseType(typeof(WarehouseStockDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves a warehouse stock by ID.")]
        [EndpointDescription("Returns detailed information about the specified product.")]
        [EndpointName("GetWarehouseStockById")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
        public async Task<IActionResult> GetById(Guid warehouseStockId, CancellationToken ct)
        {
            var result = await sender.Send(new GetWarehouseStockByIdQuery(warehouseStockId), ct);
            return result.Match(response => Ok(response), Problem);
        }



        [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new warehousestock.")]
    [EndpointDescription("Adds a new warehousestock to the system.")]
    [EndpointName("AddWarehourProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> AddWarehouse([FromBody] AddWarehouseProductRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new AddWarehourProductCommand() {
              WarehousesId =  request.WarehouseId,
              Product = new CreateProductCommand() {
                  SKU = request.Product.SKU,
                  BarCode = request.Product.BarCode,
                  ProductName = request.Product.ProductName,
                  Description = request.Product.Description,
                  SellingPrice = request.Product.SellingPrice,
                  IsActive = request.Product.IsActive,
                  Unit = (Domain.Products.Enums.Unit)request.Product.Unit,
                  CategoryId = request.Product.CategoryId
              }
            }, ct);


            return result.Match(
                _ => StatusCode(StatusCodes.Status201Created),
                Problem);
        }
    
    [HttpPut("{warehouseStockId:guid}/minimum-level")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates warehousestock minimum level.")]
    [EndpointDescription("Updates the minimum level for the specified warehousestock.")]
    [EndpointName("UpdateWarehouseStockMinimumLevel")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> UpdateMinimumLevel(Guid warehouseStockId, [FromBody] UpdateWarehouseStockMinimumLevelRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateWarehouseStockMinimumLevelCommand() {
               Id =  warehouseStockId,
               MinimumStockLevel = request.MinimumStockLevel}, ct);

            return result.Match(
                _ => Ok(),
                Problem);
        }


    [HttpDelete("{warehouseStockId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a warehousestock.")]
    [EndpointDescription("Deletes the specified warehousestock.")]
    [EndpointName("DeleteWarehouseStock")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid warehouseStockId, CancellationToken ct)
        {
            var result = await sender.Send(new DeleteWarehouseStockCommand(warehouseStockId), ct);
            return result.Match(_ => NoContent(), Problem);
        }



    }
}
