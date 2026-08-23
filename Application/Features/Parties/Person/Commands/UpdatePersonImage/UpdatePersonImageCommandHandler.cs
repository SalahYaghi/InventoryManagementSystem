using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Person.Commands.UpdatePersonImage
{
    public class UpdatePersonImageCommandHandler(IAppDbContext context ,
        IFileStorage fileStorage,
        ILogger<UpdatePersonImageCommandHandler> logger,
        ICachingService cache) :   // [FIX 1.12] this handler did no cache invalidation at all
        IRequestHandler<UpdatePersonImageCommand, Result<Updated>>
    {
        private readonly ILogger<UpdatePersonImageCommandHandler> _logger = logger;

        public async Task<Result<Updated>> Handle(UpdatePersonImageCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(UpdatePersonImageCommandHandler));


            var entity = await context.People
                .Include(p => p.Document)
                .FirstOrDefaultAsync(x => x.Id == request.PersonId, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("UpdatePersonImageCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Person.NotFound\", \"Person was not found.\")");
                return Error.NotFound("Person.NotFound", "Person was not found.");

            }

            var previousImageUrl = entity.ImageUrl;

            if (request.Image is null)
            {
                entity.UpdateImageUrl(null);
            }
            else
            {
                var result = await fileStorage.SaveFile(request.Image!
                 , DefaultDirectory.DefaultPeopleDirectory, cancellationToken);

                if (result.IsError)

                {

                    _logger.LogError("UpdatePersonImageCommandHandler stopped because an error result was returned: {ErrorResult}.", "result.Errors");
                    return result.Errors;

                }
                if (string.IsNullOrEmpty(result.Value))
                {
                    _logger.LogError("UpdatePersonImageCommandHandler stopped because an error result was returned: {ErrorResult}.", "Error.Failure(\"FileStorage.Failure\", \"Failed to save the file.\")");
                    return Error.Failure("FileStorage.Failure", "Failed to save the file.");
                }

                entity.UpdateImageUrl(result.Value);
            }

            await context.SaveChangesAsync(cancellationToken);

            if (!string.IsNullOrEmpty(previousImageUrl))
                fileStorage.DeleteFile(previousImageUrl);

            await cache.RemoveByTagAsync(CacheFanout.Expand(CacheEntities.Person), cancellationToken);

            _logger.LogInformation("UpdatePersonImageCommandHandler completed successfully.");
            return Result.Updated;
        }
    }
}

