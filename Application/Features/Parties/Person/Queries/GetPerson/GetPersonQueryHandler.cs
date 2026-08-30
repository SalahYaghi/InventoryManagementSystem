using Contract.Common.Interfaces;
using Contract.Features.Parties.People.DTOs;
using Contract.Features.Parties.People.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Domain.Contacts.Address.Country;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.People.Queries.GetPerson
{
    public sealed class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Result<PersonDto>>
    {
        private readonly ILogger<GetPersonQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetPersonQueryHandler(IAppDbContext context,
            ILogger<GetPersonQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<PersonDto>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetPersonQueryHandler));

            
            var entity = await _context.People
                .Include(p => p.Contact)
                .Include(p => p.Address)
                    .ThenInclude(a => a!.City)
                .Include(p => p.Address)
                    .ThenInclude(a => a!.Country)
                .Include(p => p.Document)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetPersonQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Person.NotFound\", \"Person was not found.\")");
                return Error.NotFound("Person.NotFound", "Person was not found.");

            }

            _logger.LogInformation("GetPersonQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

