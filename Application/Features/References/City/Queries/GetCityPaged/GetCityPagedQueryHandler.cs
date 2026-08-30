using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.References.Cities.DTOs;
using Contract.Features.References.Cities.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Cities.Queries.GetCityPaged
{
    public sealed class GetCityPagedQueryHandler : IRequestHandler<GetCityByCountryIdPagedQuery, Result<List<CityDto>>>
    {
        private readonly ILogger<GetCityPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCityPagedQueryHandler(IAppDbContext context,
            ILogger<GetCityPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<CityDto>>> Handle(GetCityByCountryIdPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCityPagedQueryHandler));

            var query = _context.Cities
                .AsNoTracking()
                .Where(c => c.CountryId == request.CountryId)
                .OrderBy(x => x.Name)
                .Select(x => new CityDto() { 
                        Id = x.Id,
                        Name = x.Name,
                });

            var result = await query.ToListAsync(
                cancellationToken);

            _logger.LogInformation("GetCityPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

