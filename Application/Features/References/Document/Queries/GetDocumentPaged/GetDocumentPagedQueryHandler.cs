using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.References.Documents.DTOs;
using Contract.Features.References.Documents.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Documents.Queries.GetDocumentPaged
{
    public sealed class GetDocumentPagedQueryHandler : IRequestHandler<GetDocumentPagedQuery, Result<PaginatedList<DocumentDto>>>
    {
        private readonly ILogger<GetDocumentPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetDocumentPagedQueryHandler(IAppDbContext context,
            ILogger<GetDocumentPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PaginatedList<DocumentDto>>> Handle(GetDocumentPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetDocumentPagedQueryHandler));

            var query = _context.Documents
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Select(x => x.ToDto());

            var result = await query.ToPaginatedListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation("GetDocumentPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

