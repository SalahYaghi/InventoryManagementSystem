using Contract.Common.Interfaces;
using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.Parties.Customers.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MechanicShop.Domain.Common.Results;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Customers.Queries.GetCustomer
{
    public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Result<CustomerDto>>
    {
        private readonly ILogger<GetCustomerQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCustomerQueryHandler(IAppDbContext context,
            ILogger<GetCustomerQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCustomerQueryHandler));

            var entity = await _context.Customers
                .Include(c => c.Address)
                    .ThenInclude(c => c!.Country)
                .Include(c => c.Address)
                    .ThenInclude(c => c!.City)
                .Include(c => c.Contact)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetCustomerQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Customer.NotFound\", \"Customer was not found.\")");
                return Error.NotFound("Customer.NotFound", "Customer was not found.");

            }

            _logger.LogInformation("GetCustomerQueryHandler completed successfully.");
            return entity.ToDto();
        }
    }
}

