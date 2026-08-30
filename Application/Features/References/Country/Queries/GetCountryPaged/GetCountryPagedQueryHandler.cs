using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.References.Countries.DTOs;
using Contract.Features.References.Countries.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.References.Countries.Queries.GetCountryPaged
{
    public sealed class GetCountryPagedQueryHandler : IRequestHandler<GetCountryPagedQuery, Result<List<CountryDto>>>
    {
        private readonly ILogger<GetCountryPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCountryPagedQueryHandler(IAppDbContext context,
            ILogger<GetCountryPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<CountryDto>>> Handle(GetCountryPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCountryPagedQueryHandler));

            var query = _context.Countries
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(x => new CountryDto() { 
                    Name = x.Name , 
                    Id   = x.Id ,
                });

            var result = await query.ToListAsync(
                cancellationToken);

            _logger.LogInformation("GetCountryPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

