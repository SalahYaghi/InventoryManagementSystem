using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.References.Documents.DTOs;

namespace Contract.Features.References.Documents.Queries.GetDocument
{
    public sealed record GetDocumentQuery(Guid Id) : ICachedQuery<Result<DocumentDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.References, CacheEntities.Document, nameof(GetDocumentQuery), Id);
        public string[] Tags => [CacheEntities.Document];
    }
}

