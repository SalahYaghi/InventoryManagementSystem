using Domain.Common.Constants;
using Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Inventory.Adjustment.Commands.UpdateAdjustment;
using Application.Features.Inventory.Adjustment.Commands.UpdateAdjustmentDetailsQuantity;
using Application.Features.Inventory.Adjustment.Queries.GetAdjustmentDetail;
using Application.Features.Inventory.Adjustment.Queries.GetAdjustmentDetailPaged;
using Application.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail; 
using Application.Features.Inventory.Adjustments.Commands.CreateAdjustment;
using Application.Features.Inventory.Adjustments.Commands.DeleteAdjustment;
using Application.Features.Inventory.Adjustments.Queries.GetAdjustment;
using Application.Features.Inventory.Adjustments.Queries.GetAdjustmentPaged;
using Application.Features.Inventory.Product.Commands.DeleteProduct;
using Application.Features.Inventory.Product.Commands.UpdateProduct;
using Application.Features.Inventory.Product.DTOs;
using Application.Features.Inventory.Product.Queries.GetProduct;
using Application.Features.Inventory.Product.Queries.GetProductPaged;
using Application.Features.Transactions.Order.Commands.CreateOrderDetail;
using Application.Features.Transactions.Order.Commands.DeleteOrderDetail;
using Application.Features.Transactions.Orders.Commands.UpdateOrder;
using Contracts.Requests.Adjustment;
using Contracts.Requests.Adjustments;
using Contracts.Requests.Orders;
using Contracts.Requests.Products;
using Domain.Adjustments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Adjustments")]
    [ApiVersion("1.0")]
    [Authorize]
public class AdjustmentsController(IMediator sender) : ApiController
    {
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Adjustment])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged adjustments.")]
    [EndpointDescription("Returns a paginated list of adjustments.")]
    [EndpointName("GetAdjustments")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await sender.Send(new GetAdjustmentPagedQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
                
            }, ct);

            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("{AdjustmentId:guid}", Name = "GetAdjustmentById")]
    [OutputCache(Tags = [CacheEntities.Adjustment])]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves an adjustment by ID.")]
    [EndpointDescription("Returns detailed information about the specified adjustment.")]
    [EndpointName("GetAdjustmentById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid AdjustmentId, CancellationToken ct)
        {
            var result = await sender.Send(new GetAdjustmentQuery(AdjustmentId), ct);
            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("{AdjustmentId:guid}/adjustment-details")]
    [OutputCache(Tags = [CacheEntities.AdjustmentDetail])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves adjustment details.")]
    [EndpointDescription("Returns details for the specified adjustment.")]
    [EndpointName("GetAdjustments")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetAdjustmentDetails(Guid AdjustmentId, CancellationToken ct = default)
        {
            var result = await sender.Send(new GetAdjustmentDetailPagedQuery(AdjustmentId) {
              
            }
            , ct);

            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("adjustment-details/{detailId:guid}")]
    [OutputCache(Tags = [CacheEntities.AdjustmentDetail])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves adjustment details.")]
    [EndpointDescription("Returns details for the specified adjustment.")]
    [EndpointName("GetAdjustmentDetail")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetAdjustmentDetailById(Guid detailId,CancellationToken ct = default)
        {
            var result = await sender.Send(new GetAdjustmentDetailQuery(detailId)
            , ct);

            return result.Match(response => Ok(response), Problem);
        }
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new adjustment.")]
    [EndpointDescription("Adds a new adjustment to the system.")]
    [EndpointName("CreateAdjustment")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> CreateAdjustment(CreateAdjustmentRequest AdjustmentRequest)
        {
            var result = await sender.Send(new CreateAdjustmentCommand()
            {
                AdjustmentReason = (AdjustmentReason)AdjustmentRequest.AdjustmentReason,

                AdjustmentType = (AdjustmentRequest.AdjustmentType is not null) ? (AdjustmentType)AdjustmentRequest.AdjustmentType : null ,
                Notes = AdjustmentRequest.Notes,
               WarehouseId = AdjustmentRequest.WarehouseId,
                AdjustmentDetailCommands = AdjustmentRequest.AdjustmentDetails.Select(o => new CreateAdjustmentDetailInnerCommand()
                {
                    RowVersion = o.RowVersion ,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
        

                }).ToList()
            });

            return result.Match(
                response => CreatedAtRoute("GetAdjustmentById", new { version = "1.0" , AdjustmentId = response.Id  } , response),
                Problem
            );
        }
    [HttpPut("{AdjustmentId:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates an adjustment.")]
    [EndpointDescription("Updates the specified adjustment.")]
    [EndpointName("UpdateAdjustment")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> UpdateAdjustment(Guid AdjustmentId, [FromBody] UpdateAdjustmentRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateAdjustmentCommand
            {
                Id = AdjustmentId,
                Notes = request.Notes,
                
            }, ct);

            return result.Match(response => NoContent(), Problem);
        }
    [HttpPut("{AdjustmentId:guid}/status")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates adjustment status.")]
    [EndpointDescription("Updates the status of the specified adjustment.")]
    [EndpointName("UpdateAdjustmentStatus")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> UpdateAdjustmentStatus(Guid AdjustmentId, [FromBody] UpdateAdjustmentStatusRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateAdjustmentStatusCommand
            {
                Id = AdjustmentId,
                AdjustmentStatus = (AdjustmentStatus)request.AdjustmentStatus
            }, ct);

            return result.Match(_ => NoContent(), Problem);
        }
    [HttpDelete("{AdjustmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes an adjustment.")]
    [EndpointDescription("Deletes the specified adjustment.")]
    [EndpointName("DeleteAdjustment")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteAdjustment(Guid AdjustmentId, CancellationToken ct)
        {
            var result = await sender.Send(new DeleteAdjustmentCommand(AdjustmentId), ct);
            return result.Match(_ => NoContent(), Problem);
        }
    [HttpDelete("adjustment-details/{detailId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes an adjustment.")]
    [EndpointDescription("Deletes the specified adjustment.")]
    [EndpointName("DeleteAdjustmentDetail")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteAdjustmentDetail(Guid detailId , CancellationToken ct) {

            var result = await sender.Send(new DeleteAdjustmentDetailCommand(detailId), ct);
            return result.Match(_ => NoContent(), Problem);

        }
    [HttpPut("adjustment-details/{detailId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates an adjustment.")]
    [EndpointDescription("Updates the specified adjustment.")]
    [EndpointName("UpdateAdjustmentDetailQuantity")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> UpdateAdjustmentDetailQuantity(Guid detailId,
            [FromBody] UpdateAdjustmentDetailQuantityRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateAdjustmentDetailQuantityCommand
            {
                Id = detailId,
                RowVersion = request.RowVersion,
                Quantity = request.Quantity,
               
            }, ct);

            return result.Match(_ => NoContent(), Problem);
        }
    [HttpPost("adjustment-details")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new adjustment.")]
    [EndpointDescription("Adds a new adjustment to the system.")]
    [EndpointName("CreateAdjustmentDetail")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> CreateAdjustmentDetail(CreateAdjustmentDetailRequest AdjustmentRequest)
        {
            var result = await sender.Send(new CreateAdjustmentDetailCommand()
            {
               AdjustmentId = AdjustmentRequest.AdjustmentId,
               Quantity = AdjustmentRequest.Quantity,
               ProductId = AdjustmentRequest.ProductId,
               RowVersion = AdjustmentRequest.RowVersion,
               
                
            });

            return result.Match(
                response => CreatedAtRoute("GetAdjustmentById", new { version = "1.0", AdjustmentId = response.Id }, response),
                Problem
            );
        }


    }
}
