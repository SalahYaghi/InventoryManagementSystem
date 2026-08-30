using Contract.Common.Errors;
using Contract.Common.Files;
using Contract.Common.Interfaces;
using Domain.People;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Person.Queries.GetPersonImage
{
    public class GetPersonImageQueryHandler(IAppDbContext context , IFileStorage storage,
        ILogger<GetPersonImageQueryHandler> logger) : IRequestHandler<GetPersonImageQuery, Result<FileDto>>
    {
        private readonly ILogger<GetPersonImageQueryHandler> _logger = logger;

        public async Task<Result<FileDto>> Handle(GetPersonImageQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetPersonImageQueryHandler));


            var person = await context.People.FirstOrDefaultAsync(p => p.Id == request.PersonId);

            if (person == default)
            {
                _logger.LogWarning("GetPersonImageQueryHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.PersonNotFound");
                return ApplicationErrors.PersonNotFound;
            }
            if (string.IsNullOrEmpty(person.ImageUrl))
            {
                _logger.LogWarning("GetPersonImageQueryHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.ImageNotFound");
                return ApplicationErrors.ImageNotFound;
            }

            return new FileDto()
            {
                FileUrl = person.ImageUrl,
                ContentType = storage.GetMimeType(person.ImageUrl ?? string.Empty) , 
                FileName = "PersonImage"
            };
        }
    }
}

