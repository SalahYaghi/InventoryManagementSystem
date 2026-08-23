using Contract.Common.Constants;
using Contract.Features.Inventory.Product.Commands.DeleteProduct;
using Contract.Features.Inventory.Product.Commands.UpdateProduct;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Inventory.Product.Queries.GetProduct;
using Contract.Features.Inventory.Product.Queries.GetProductPaged;
using Contract.Features.Transactions.Order.Commands.DeleteOrderDetail;
using Contract.Features.Transactions.Order.Commands.UpdateOrderDetail;
using Contract.Features.Transactions.Order.Queries.GetOrderDetail;
using Contract.Features.Transactions.Order.Queries.GetOrderDetailPaged;
using Contract.Features.Transactions.OrderDetails.Commands.CreateOrderDetail;
using Contract.Features.Transactions.Orders.Commands.CreateOrder;
using Contract.Features.Transactions.Orders.Commands.DeleteOrder;
using Contract.Features.Transactions.Orders.Commands.UpdateOrder;
using Contract.Features.Transactions.Orders.DTOs;
using Contract.Features.Transactions.Orders.Queries.GetOrder;
using Contract.Features.Transactions.Orders.Queries.GetOrderPaged;
using Contract.Requests.Orders;
using Contract.Requests.Orders;
using Contract.Requests.Products;
using Domain.Common.Constants;
using Domain.Orders;
using Infrastructure.Policies.OutputCachePolicies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/v{version:apiVersion}/orders")]
    [ApiVersion("1.0")]
    [Authorize]
public class OrdersController(IMediator sender) : ApiController
    {
    [HttpGet]
     [OutputCache(Tags = [CacheEntities.Order], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByQueryKeys = ["pageNumber", "pageSize", "orderType"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged orders.")]
    [EndpointDescription("Returns a paginated list of orders.")]
    [EndpointName("GetOrders")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery]OrderType? orderType = null ,CancellationToken ct = default)
        {
            var result = await sender.Send(new GetOrderPagedQuery
            {
                OrderType = orderType,
                PageNumber = pageNumber,
                PageSize = pageSize
            }, ct);

            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("{orderId:guid}", Name = "GetOrderById")]
    [OutputCache(Tags = [CacheEntities.Order], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["orderId"])]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves an order by ID.")]
    [EndpointDescription("Returns detailed information about the specified order.")]
    [EndpointName("GetOrderById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid orderId, CancellationToken ct)
        {
            var result = await sender.Send(new GetOrderQuery(orderId), ct);
            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("{orderId:guid}/order-details")]
    [OutputCache(Tags = [CacheEntities.OrderDetail], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByQueryKeys = ["pageNumber", "pageSize"], VaryByRouteValueNames = ["orderId"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves order details.")]
    [EndpointDescription("Returns details for the specified order.")]
    [EndpointName("GetOrderDetails")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetOrderDetails(Guid orderId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await sender.Send(new GetOrderDetailPagedQuery(orderId)
            , ct);

            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("order-details/{detailId:guid}")]
    [OutputCache(Tags = [CacheEntities.OrderDetail], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByQueryKeys = ["pageNumber", "pageSize"], VaryByRouteValueNames = ["detailId"])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves order details.")]
    [EndpointDescription("Returns details for the specified order.")]
    [EndpointName("GetOrderDetail")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetOrderDetailById(Guid detailId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var result = await sender.Send(new GetOrderDetailQuery(detailId)
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
    [EndpointSummary("Creates a new order.")]
    [EndpointDescription("Adds a new order to the system.")]
    [EndpointName("CreateOrder")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest orderRequest)
        {
            var result = await sender.Send(new CreateOrderCommand()
            {
                CustomerId = orderRequest.CustomerId,
                OrderType = (OrderType)orderRequest.OrderType,
                Discount = orderRequest.Discount,
                DestinationWarehouseId = orderRequest.DestinationWarehouseId,
                DueDate = orderRequest.DueDate,
                Notes = orderRequest.Notes,
                SourceWarehouseId = orderRequest.SourceWarehouseId,
                SupplierId = orderRequest.SupplierId,
                OrderDetails = orderRequest.OrderDetails.Select(o => new CreateOrderDetailCommand()
                {
                    RowVersion = o.RowVersion ,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity
                }).ToList()
            });

            return result.Match(
                response => CreatedAtRoute("GetOrderById", new { version = "1.0" , orderId = response.Id  } , response),
                Problem
            );
        }
    [HttpPut("{orderId:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates an order.")]
    [EndpointDescription("Updates the specified order.")]
    [EndpointName("UpdateOrder")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] UpdateOrderRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateOrderCommand
            {
                Id = orderId,
                Notes = request.Notes,
                DiscountAmount = request.DiscountAmount,
                DueDate = request.DueDate
            }, ct);

            return result.Match(response => NoContent(), Problem);
        }
    [HttpPut("{orderId:guid}/status")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates order status.")]
    [EndpointDescription("Updates the status of the specified order.")]
    [EndpointName("UpdateOrderStatus")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateOrderStatusCommand
            {
                Id = orderId,
                OrderStatus = (OrderStatus)request.OrderStatus
            }, ct);

            return result.Match(_ => NoContent(), Problem);
        }
    [HttpDelete("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes an order.")]
    [EndpointDescription("Deletes the specified order.")]
    [EndpointName("DeleteOrder")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteOrder(Guid orderId, CancellationToken ct)
        {
            var result = await sender.Send(new DeleteOrderCommand(orderId), ct);
            return result.Match(_ => NoContent(), Problem);
        }
    [HttpDelete("order-details/{detailId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes an order.")]
    [EndpointDescription("Deletes the specified order.")]
    [EndpointName("DeleteOrderDetail")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteOrderDetail(Guid detailId , CancellationToken ct) {

            var result = await sender.Send(new DeleteOrderDetailCommand(detailId), ct);
            return result.Match(_ => NoContent(), Problem);

        }
    [HttpPut("order-details/{detailId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates an order.")]
    [EndpointDescription("Updates the specified order.")]
    [EndpointName("UpdateOrderDetail")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> UpdateOrderDetailQuantity(Guid detailId, [FromBody] UpdateOrderDetailQuantityRequest request, CancellationToken ct)
        {
            var result = await sender.Send(new UpdateOrderDetailCommand
            {
                Id = detailId,
                Quantity = request.Quantity,
                RowVersion = request.RowVersion
            }, ct);

            return result.Match(_ => NoContent(), Problem);
        }
    [HttpPost("order-details")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new order.")]
    [EndpointDescription("Adds a new order to the system.")]
    [EndpointName("CreateOrderDetailOrder")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser)]
    public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailRequest orderRequest)
        {
            var result = await sender.Send(new 
                Contract.Features.Transactions.Order.Commands.CreateOrderDetail.CreateOrderDetailCommand()
            {
               OrderId = orderRequest.OrderId,
               Quantity = orderRequest.Quantity,
               ProductId = orderRequest.ProductId,
               RowVersion = orderRequest.RowVersion,
               
            });

            return result.Match(
                response => CreatedAtRoute("GetOrderById", new { version = "1.0", orderId = response.Id }, response),
                Problem
            );
        }


    }
}
