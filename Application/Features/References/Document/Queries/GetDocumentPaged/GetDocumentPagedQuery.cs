using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.Documents.DTOs;

namespace Contract.Features.References.Documents.Queries.GetDocumentPaged
{
    public sealed record GetDocumentPagedQuery : ICachedQuery<Result<PaginatedList<DocumentDto>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;

        public string CacheKey => CacheKeys.ForEntityPaged(CacheGroups.References, CacheEntities.Document, nameof(GetDocumentPagedQuery), PageNumber, PageSize);
        public string[] Tags => [CacheEntities.Document];
    }
}

