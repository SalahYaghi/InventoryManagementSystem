using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.References.Documents.DTOs;
using Microsoft.AspNetCore.Http;

namespace Contract.Features.References.Documents.Commands.UpdateDocument
{
    public sealed record UpdateDocumentCommand : IRequest<Result<DocumentDto>>
    {
        public Guid? Id { get; init; }
        public Domain.Document.DocumentType DocumentType { get; init; }
        public IFormFile? Image { get; init; } 
    }
}

