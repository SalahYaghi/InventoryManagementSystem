using Contract.Common.Constants;
using Contract.Common.Functions;
using Contract.Common.Interfaces;
using Contract.Features.References.Document;
using Contract.Features.References.Documents.DTOs;
using Contract.Features.References.Documents.Mappers;
using Domain.Document;
 using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Documents.Commands.CreateDocument
{
    public sealed class CreatePersonDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, Result<DocumentDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreatePersonDocumentCommandHandler> _logger;
        private readonly IFileStorage _fileStorage;

        public CreatePersonDocumentCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreatePersonDocumentCommandHandler> logger , 
            IFileStorage fileStorage)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _fileStorage = fileStorage;
        }

        public async Task<Result<DocumentDto>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreatePersonDocumentCommandHandler));

            string imageUrl = "";

            var imageResult = await ImageFunctions.Save(request.DocumentImage,
                DefaultDirectory.DefaultPeopleDocumentsDirectory , 
                _logger , _fileStorage);

            if (imageResult.IsError)
            {
                _logger.LogError("Failed to save document image. Errors: {Errors}", string.Join(", ", imageResult.Errors.Select(e => e.Description)));
                return imageResult.Errors;
            }
 
            imageUrl = imageResult.Value;

            var entityResult = Domain.Document.Document.Create(Guid.NewGuid(),request.DocumentType, imageUrl);

            if (entityResult.IsError)
            {
                _logger.LogError("Failed to create document entity. Errors: {Errors}", 
                    string.Join(", ", entityResult.Errors.Select(e => e.Description)));
                return entityResult.Errors;
            }
            _context.Documents.Add(entityResult.Value);
            _logger.LogInformation("CreatePersonDocumentCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreatePersonDocumentCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.Document, CacheEntities.Person), cancellationToken);

            _logger.LogInformation("Document created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

