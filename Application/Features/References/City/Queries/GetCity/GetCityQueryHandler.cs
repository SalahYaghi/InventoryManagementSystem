using Contract.Common.Interfaces;
using Contract.Features.References.Cities.DTOs;
using Contract.Features.References.Cities.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Cities.Queries.GetCity
{
    public sealed class GetCityQueryHandler : IRequestHandler<GetCityQuery, Result<CityDto>>
    {
        private readonly ILogger<GetCityQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCityQueryHandler(IAppDbContext context,
            ILogger<GetCityQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<CityDto>> Handle(GetCityQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCityQueryHandler));

            var entity = await _context.Cities
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetCityQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"City.NotFound\", \"City was not found.\")");
                return Error.NotFound("City.NotFound", "City was not found.");

            }

            _logger.LogInformation("GetCityQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

