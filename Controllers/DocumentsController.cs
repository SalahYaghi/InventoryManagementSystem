using Domain.Common.Constants;
using Application.Common.Constants;
using Application.Features.Parties.Person.Queries.GetPersonImage;
 using Application.Features.References.Documents.Commands.CreateDocument;
using Application.Features.References.Documents.Commands.DeleteDocument;
using Application.Features.References.Documents.Commands.UpdateDocument;
using Application.Features.References.Documents.DTOs;
using Application.Features.References.Documents.Queries.GetDocument;
using Application.Features.References.Documents.Queries.GetDocumentPaged;
using Contracts.Requests.Documents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystemAPI.Controllers;

[Route("api/v{version:apiVersion}/documents")]
[ApiVersion("1.0")]
[Authorize]
public sealed class DocumentsController(ISender sender) : ApiController
{
    [HttpGet]
    [OutputCache(Tags = [CacheEntities.Document])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves paged documents.")]
    [EndpointDescription("Returns a paginated list of documents.")]
    [EndpointName("GetDocuments")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await sender.Send(new GetDocumentPagedQuery { PageNumber = pageNumber, PageSize = pageSize }, ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{documentId:guid}", Name = "GetDocumentById")]
    [OutputCache(Tags = [CacheEntities.Document])]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a document by ID.")]
    [EndpointDescription("Returns detailed information about the specified document.")]
    [EndpointName("GetDocumentById")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetById(Guid documentId, CancellationToken ct)
    {
        var result = await sender.Send(new GetDocumentQuery(documentId), ct);
        return result.Match(response => Ok(response), Problem);
    }

    [HttpGet("{documentId:guid}/image", Name = "GetDocumentImageById")]
    [OutputCache(Tags = [CacheEntities.Document])]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves document image.")]
    [EndpointDescription("Returns the image associated with the specified document.")]
    [EndpointName("GeDocumentImage")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Viewer)]
    public async Task<IActionResult> GetImageById(Guid documentId, CancellationToken ct)
    {
        var result = await sender.Send(new GeDocumentImageQuery(documentId), ct);
        return result.Match(response => PhysicalFile(response.FileUrl!, response.ContentType), Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Creates a new document.")]
    [EndpointDescription("Adds a new document to the system.")]
    [EndpointName("CreateDocument")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Create([FromForm] CreateDocumentRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new CreateDocumentCommand
        {
            DocumentType = (Domain.Document.DocumentType)request.DocumentType,
            DocumentImage = request.DocumentImage
        }, ct);

        return result.Match(
            response => CreatedAtRoute("GetDocumentById", new { version = "1.0", documentId = response.Id }, response),
            Problem);
    }

    [HttpPut("{documentId:guid}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Updates a document.")]
    [EndpointDescription("Updates the specified document.")]
    [EndpointName("UpdateDocument")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Update(Guid documentId, [FromForm] UpdateDocumentRequest request, CancellationToken ct)
    {
        var result = await sender.Send(new UpdateDocumentCommand
        {
            Id = documentId,
            DocumentType = (Domain.Document.DocumentType)request.DocumentType,
            Image = request.Image
        }, ct);

        return result.Match(response => Ok(response), Problem);
    }

    [HttpDelete("{documentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Removes a document.")]
    [EndpointDescription("Deletes the specified document.")]
    [EndpointName("DeleteDocument")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> Delete(Guid documentId, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteDocumentCommand(documentId), ct);
        return result.Match(_ => NoContent(), Problem);
    }
}
