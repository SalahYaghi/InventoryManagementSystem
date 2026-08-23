using Domain.Common.Constants;
using Application.Common.Files;
 using Application.Features.Inventory.Product.Commands.CreateProduct;
using Application.Features.Inventory.Product.Commands.CreateProductImage;
using Application.Features.Inventory.Product.Commands.DeleteProduct;
using Application.Features.Inventory.Product.Commands.UpdateProduct;
using Application.Features.Inventory.Product.DTOs;
using Application.Features.Inventory.Product.Queries.GetAllProductImages;
using Application.Features.Inventory.Product.Queries.GetProduct;
using Application.Features.Inventory.Product.Queries.GetProductPaged;
using Contracts.Requests.People;
using Contracts.Requests.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Application.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0")]
[Authorize]
public sealed class ProductsController(ISender sender) : ApiController
{
    [HttpPost("{productId:guid}/image")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates product image.")]
    [EndpointDescription("Uploads an image for the specified product.")]
    [EndpointName("CreateProductImage")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser)]
    public async Task<IActionResult> CreateImage(Guid productId, [FromForm] CreateProductImageRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateProductImageCommand(productId, request.Image), ct);

        return result.Match(
            _ => StatusCode(StatusCodes.Status201Created),
            Problem);
    }

    [HttpGet("{productId:guid}/images")]
    [Produces("image/jpeg", "image/png", "application/octet-stream")]
    [OutputCache(Tags = [CacheEntities.Image])]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves product image.")]
    [EndpointDescription("Returns the image associated with the specified product.")]
    [EndpointName("GetAllProductImages")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetImages(Guid productId, CancellationToken ct)
    {
        var result = await sender.Send(new GetAllProductImagesQuery(productId), ct);

       
        return result.Match(
            response => {
                response.Stream?.Position = 0;
                return File(response.Stream!, response.ContentType); },
            Problem);
    }

    [HttpDelete("{productId:guid}/image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Deletes product image.")]
    [EndpointDescription("Deletes the image associated with the specified product.")]
    [EndpointName("DeleteProductImage")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> DeleteImage(Guid productId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteProductImageCommand(productId), ct);

        return result.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Product])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged products.")]
    [EndpointDescription("Returns a paginated list of products.")]
    [EndpointName("GetProducts")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10, [FromQuery]Guid? excludeSupplierId = null,
        [FromQuery] List<Guid>? excludeProductsIds = null ,
        [FromQuery] Guid? fromWarehouseId = null ,
        [FromQuery] Guid? fromSupplierId = null
        , CancellationToken ct = default)
    {
        var result = await sender.Send(new GetProductPagedQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            ExcludeSupplierId = excludeSupplierId,
            excludeProductsIds = excludeProductsIds,
            fromSupplierId = fromSupplierId,
            fromWarehouseId = fromWarehouseId,
          
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{productId:guid}", Name = "GetProductById")]
    [OutputCache(Tags = [CacheEntities.Product])]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a product by ID.")]
    [EndpointDescription("Returns detailed information about the specified product.")]
    [EndpointName("GetProductById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.SalesUser + "," + RoleConstants.PurchasesUser + "," + RoleConstants.WarehouseUser + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid productId, CancellationToken ct)
    {
        var result = await sender.Send(new GetProductQuery(productId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new product.")]
    [EndpointDescription("Adds a new product to the system.")]
    [EndpointName("CreateProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateProductCommand
        {
            SKU = request.SKU,
            BarCode = request.BarCode,
            ProductName = request.ProductName,
            Description = request.Description,
            SellingPrice = request.SellingPrice,
            IsActive = request.IsActive,
            Unit = (Domain.Products.Enums.Unit)request.Unit,
            CategoryId = request.CategoryId
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetProductById", new { version = "1.0", productId = response.Id }, response),
            Problem);
    }

    [HttpPut("{productId:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a product.")]
    [EndpointDescription("Updates the specified product.")]
    [EndpointName("UpdateProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.PurchasesUser)]
    public async Task<IActionResult> Update(Guid productId, [FromBody] UpdateProductRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateProductCommand
        {
            Id = productId,
            SKU = request.SKU,
            BarCode = request.BarCode,
            ProductName = request.ProductName,
            Description = request.Description,
            SellingPrice = request.SellingPrice,
            IsActive = request.IsActive,
            Unit = (Domain.Products.Enums.Unit)request.Unit,
            CategoryId = request.CategoryId
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a product.")]
    [EndpointDescription("Deletes the specified product.")]
    [EndpointName("DeleteProduct")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid productId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteProductCommand(productId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
