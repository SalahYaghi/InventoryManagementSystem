using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.References.Document;
using Contract.Features.References.Documents.DTOs;
using Contract.Features.References.Documents.Mappers;
 using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Documents.Commands.UpdateDocument
{
    public sealed class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, Result<DocumentDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<UpdateDocumentCommandHandler> _logger;
        private readonly IFileStorage _fileStorage;
        public UpdateDocumentCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<UpdateDocumentCommandHandler> logger , 
            IFileStorage fileStorage)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _fileStorage = fileStorage;
        }

        public async Task<Result<DocumentDto>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdateDocumentCommandHandler));


            var entity = await _context.Documents.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdateDocumentCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Document.NotFound\", \"Document was not found.\")");
                return Error.NotFound("Document.NotFound", "Document was not found.");

            }

            string oldPath = entity.ImageUrl;

            string imageUrl = "";

            if (request.Image != null)
            {
                var imageUrlResult = await _fileStorage.SaveFile(request.Image,
                    DefaultDirectory.DefaultPeopleDocumentsDirectory, cancellationToken);

                if (imageUrlResult.IsError)
                {
                    _logger.LogError("Error saving image when creating a document: {Errors}", imageUrlResult.Errors);
                    return DocumentApplicationErrors.ErrorSavingImage;
                }

                imageUrl = imageUrlResult.Value;
            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogError("Error saving image when creating a document");
                return DocumentApplicationErrors.ImageFormattingError;
            }

            }


            var result = entity.Update(request.DocumentType, imageUrl);

            if(result.IsError)
            {
                _logger.LogError("Error updating document with key {Key}: {Error}", request.Id, result.Errors);
                return result.Errors;
            }

            _logger.LogInformation("UpdateDocumentCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("UpdateDocumentCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.Document, CacheEntities.Person), cancellationToken);

            _logger.LogInformation("Document updated successfully with key {Key}", request.Id);



            if (request.Image != null) 
            _fileStorage.DeleteFile(oldPath);

            _logger.LogInformation("UpdateDocumentCommandHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

