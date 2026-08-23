using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MechanicShop.Domain.Common.Results;

namespace Contract.Features.Parties.People.Commands.DeletePerson
{
    public sealed class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result<Deleted>>
    {
        private readonly IAppDbContext _context;
        private readonly ICachingService _cache;
        private readonly ILogger<DeletePersonCommandHandler> _logger;
        private readonly IFileStorage _fileStorage;
        public DeletePersonCommandHandler(
            IAppDbContext context,
            ICachingService cache,
            ILogger<DeletePersonCommandHandler> logger , 
            IFileStorage fileStorage)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
            _fileStorage = fileStorage;
        }

        public async Task<Result<Deleted>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(DeletePersonCommandHandler));

            var entity = await _context.People
                .Include(p => p.Document)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            
            if (entity is null)
            {

            
                _logger.LogWarning("DeletePersonCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Person.NotFound\", \"Person was not found.\")");
                return Error.NotFound("Person.NotFound", "Person was not found.");

            
            }
            var doc = entity.Document;  
            _logger.LogInformation("DeletePersonCommandHandler is marking entity data for persistence operation.");
            _context.People.Remove(entity);
            if (doc is not null)
            {
                _logger.LogInformation("DeletePersonCommandHandler is marking entity data for persistence operation.");
                _context.Documents.Remove(doc);
            }
            _logger.LogInformation("DeletePersonCommandHandler is saving changes to the database.");
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("DeletePersonCommandHandler saved changes to the database successfully.");
            await _cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Person), cancellationToken);
          
            if (entity.Document is not null)
            {
                _fileStorage.DeleteFile(entity.Document.ImageUrl);
            }
            if(entity.ImageUrl is not null)
            {
                _fileStorage.DeleteFile(entity.ImageUrl);
            }


            _logger.LogInformation("Person deleted successfully with key {Key}", request.Id);

            return Result.Deleted;
        }
    }
}

