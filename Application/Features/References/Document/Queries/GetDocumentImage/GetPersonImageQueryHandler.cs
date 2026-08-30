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
    public class GetDocumentImageQueryHandler(IAppDbContext context , IFileStorage storage,
        ILogger<GetDocumentImageQueryHandler> logger) : IRequestHandler<GeDocumentImageQuery, Result<FileDto>>
    {
        private readonly ILogger<GetDocumentImageQueryHandler> _logger = logger;

        public async Task<Result<FileDto>> Handle(GeDocumentImageQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetDocumentImageQueryHandler));


            var person = await context.Documents.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (person == default)
            {
                _logger.LogWarning("GetDocumentImageQueryHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.PersonNotFound");
                return ApplicationErrors.PersonNotFound;
            }
            if (string.IsNullOrEmpty(person.ImageUrl))
            {
                _logger.LogWarning("GetDocumentImageQueryHandler stopped because an error result was returned: {ErrorResult}.", "ApplicationErrors.ImageNotFound");
                return ApplicationErrors.ImageNotFound;
            }

            return new FileDto()
            {
                FileUrl = person.ImageUrl,
                ContentType = storage.GetMimeType(person.ImageUrl ?? string.Empty) , 
                FileName = "DocumentImage"
            };
        }
    }
}

