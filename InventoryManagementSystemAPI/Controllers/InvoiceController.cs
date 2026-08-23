using Domain.Common.Constants;
using Contract.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Contract.Features.Inventory.AdjustmentDetails.Commands.CreateAdjustmentDetail;
using Contract.Features.Inventory.Adjustments.Commands.CreateAdjustment;
using Contract.Features.Inventory.Adjustments.Queries.GetAdjustment;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Transactions.Invoice.Commands.CreateInvoice;
using Contract.Features.Transactions.Invoice.Queries.GetInvoice;
using Contract.Features.Transactions.Invoice.Queries.GetInvoicePDF;
using Contract.Requests.Adjustments;
using Contract.Requests.Invoices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;
using Infrastructure.Policies.OutputCachePolicies;

namespace InventoryManagementSystemAPI.Controllers
{

    [Route("api/v{version:apiVersion}/Invoices")]
    [ApiVersion("1.0")]
    [Authorize]
public class InvoiceController(IMediator sender) : ApiController
    {
    [HttpGet("{invoiceId:guid}", Name = "GetInvoiceById")]
    [OutputCache(Tags = [CacheEntities.Invoice], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["invoiceId"])]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves an invoice by ID.")]
    [EndpointDescription("Returns detailed information about the specified invoice.")]
    [EndpointName("GetInvoiceById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid InvoiceId, CancellationToken ct)
        {
            var result = await sender.Send(new GetInvoiceQuery(InvoiceId), ct);
            return result.Match(response => Ok(response), Problem);
        }
    [HttpGet("{invoiceId:guid}/pdf", Name = "GetInvoicePdfById")]
    [OutputCache(Tags = [CacheEntities.Invoice], PolicyName = nameof(AuthenticatedUserCachePolicy), VaryByRouteValueNames = ["invoiceId"])]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves invoice PDF.")]
    [EndpointDescription("Returns the PDF file for the specified invoice.")]
    [EndpointName("GetInvoicePdf")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetPdfById(Guid InvoiceId, CancellationToken ct)
        {
            var result = await sender.Send(new GetInvoicePdfQuery(InvoiceId), ct);
            return result.Match(response => File(
                response.Data , 
                response.ContentType,
                response.FileName), Problem);
        }
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new invoice.")]
    [EndpointDescription("Adds a new invoice to the system.")]
    [EndpointName("CreateInvoice")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser)]
    public async Task<IActionResult> CreateInvoice(CreateInvoiceRequest InvoiceRequest)
        {
            var result = await sender.Send(new CreateInvoiceCommand()
            {
                OrderId = InvoiceRequest.OrderId,    
            });

            return result.Match(
                response => CreatedAtRoute("GetInvoiceById", new { version = "1.0", InvoiceId = response.InvoiceId }, response),
                Problem
            );
        }




    }
}
