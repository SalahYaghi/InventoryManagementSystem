using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.References.Documents.DTOs;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Contract.Features.References.Documents.Commands.CreateDocument
{
    public sealed record CreatePersonDocumentCommand : IRequest<Result<DocumentDto>>
    {
         public Guid PersonId { get; init; }
         public CreateDocumentCommand Document { get; init; }
      }
}

