using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Documents.DTOs;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Contract.Features.References.Documents.Commands.CreateDocument
{
    public sealed record CreateDocumentCommand : IRequest<Result<DocumentDto>>
    {
         public Domain.Document.DocumentType DocumentType { get; init; }
         public IFormFile? DocumentImage { get; init; } = default!; 
    }
}
