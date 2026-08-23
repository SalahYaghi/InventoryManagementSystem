using Contract.Common.Interfaces;
using Contract.Features.References.Documents.DTOs;
using Contract.Features.References.Documents.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Documents.Queries.GetDocument
{
    public sealed class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, Result<DocumentDto>>
    {
        private readonly ILogger<GetDocumentQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetDocumentQueryHandler(IAppDbContext context,
            ILogger<GetDocumentQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<DocumentDto>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetDocumentQueryHandler));

            var entity = await _context.Documents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetDocumentQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Document.NotFound\", \"Document was not found.\")");
                return Error.NotFound("Document.NotFound", "Document was not found.");

            }

            _logger.LogInformation("GetDocumentQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

