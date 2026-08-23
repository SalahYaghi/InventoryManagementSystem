using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.References.Documents.Commands.DeleteDocument
{
    public sealed class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeleteDocumentCommandHandler> _logger;
        private readonly IFileStorage _fileStorage;
        public DeleteDocumentCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeleteDocumentCommandHandler> logger ,
            IFileStorage fileStorage)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _fileStorage = fileStorage;
        }

        public async Task<Result<Deleted>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeleteDocumentCommandHandler));

            var entity = await _context.Documents.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("DeleteDocumentCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Document.NotFound\", \"Document was not found.\")");
                return Error.NotFound("Document.NotFound", "Document was not found.");

            }

            _logger.LogInformation("DeleteDocumentCommandHandler is marking entity data for persistence operation.");
            _context.Documents.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeleteDocumentCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.Document, CacheEntities.Person), cancellationToken);
            _logger.LogInformation("DeleteDocumentCommandHandler invalidated related cache entries successfully.");

            _fileStorage.DeleteFile(entity.ImageUrl);

            _logger.LogInformation("Document deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

