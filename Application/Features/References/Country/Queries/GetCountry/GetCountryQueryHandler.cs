using Contract.Common.Interfaces;
using Contract.Features.References.Countries.DTOs;
using Contract.Features.References.Countries.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Countries.Queries.GetCountry
{
    public sealed class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, Result<CountryDto>>
    {
        private readonly ILogger<GetCountryQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCountryQueryHandler(IAppDbContext context,
            ILogger<GetCountryQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<CountryDto>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCountryQueryHandler));

            var entity = await _context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetCountryQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Country.NotFound\", \"Country was not found.\")");
                return Error.NotFound("Country.NotFound", "Country was not found.");

            }

            _logger.LogInformation("GetCountryQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

