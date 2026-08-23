using Contract.Common.Constants;
using Contract.Common.Functions;
using Contract.Common.Interfaces;
using Contract.Features.References.Document;
using Contract.Features.References.Documents.Commands.CreateDocument;
using Contract.Features.References.Documents.DTOs;
using Contract.Features.References.Documents.Mappers;
using Domain.Document;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Documents.Commands.CreatePersonDocumentsCommand
{
    public sealed class CreatePersonDocumentCommandHandler : IRequestHandler<CreatePersonDocumentCommand, Result<DocumentDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<CreatePersonDocumentCommandHandler> _logger;
        private readonly IFileStorage _fileStorage;

        public CreatePersonDocumentCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<CreatePersonDocumentCommandHandler> logger ,
            IFileStorage fileStorage    )
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _fileStorage = fileStorage;

        }

        public async Task<Result<DocumentDto>> Handle(CreatePersonDocumentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(CreatePersonDocumentCommandHandler));

            var entity = await _context.People
                .Include(x => x.Document)
               .FirstOrDefaultAsync(x => x.Id == request.PersonId, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("Attempt to create a document for non-existent person with id {PersonId}.", request.PersonId);
                return Error.NotFound("Person.NotFound", "Person was not found.");
            }

            if (entity.Document is not null)
            {
                _logger.LogWarning("Attempt to create a document for person with id {PersonId} who already has a document.", request.PersonId);
                return Error.Conflict("Person.DocumentExists",
                    "Person already has a document.");
            }

            string imageUrl = "";

            var imageResult = await ImageFunctions.Save(request.Document.DocumentImage,
                DefaultDirectory.DefaultPeopleDocumentsDirectory,
                _logger, _fileStorage);

            if (imageResult.IsError)
            {
                _logger.LogError("Failed to save document image. Errors: {Errors}", string.Join(", ", imageResult.Errors.Select(e => e.Description)));
                return imageResult.Errors;
            }

            imageUrl = imageResult.Value;
            var entityResult = Domain.Document.Document.Create(Guid.NewGuid(),
                request.Document.DocumentType, imageUrl);

            if (entityResult.IsError)
            {
                _logger.LogError("Error creating document for person with id {PersonId}. Errors: {Errors}",
                    request.PersonId, string.Join(", ", entityResult.Errors.Select(e => e.Code)));
                return entityResult.Errors;
            }
            
            var updateResult = entity.UpdateDocument(entityResult.Value);

            if (updateResult.IsError)
            {
                _logger.LogError("Error updating document for person with id {PersonId}. Errors: {Errors}",
                    request.PersonId, string.Join(", ", updateResult.Errors.Select(e => e.Code)));
                return updateResult.Errors;
            }
            _logger.LogInformation("CreatePersonDocumentCommandHandler is adding new entity data to the context.");
            await _context.Documents.AddAsync(entityResult.Value,cancellationToken); 
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("CreatePersonDocumentCommandHandler is invalidating related cache entries.");
            await _cache.RemoveByTagAsync(
                CacheFanout.Expand(CacheEntities.Document, CacheEntities.Person), cancellationToken);

            _logger.LogInformation("CreatePersonDocumentCommandHandler invalidated related cache entries successfully.");

            _logger.LogInformation("Document created successfully with key {Key}", entityResult.Value.Id);

            return entityResult.Value.ToDto();
        }
    }
}

