using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.Parties.Customers.Mappers;
using Contract.Features.Parties.Supplier.DTOs;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Customers.Queries.GetCustomerPaged
{
    public sealed class GetCustomerPagedQueryHandler : IRequestHandler<GetCustomerQuery, Result<List<CustomerForListDto>>>
    {
        private readonly ILogger<GetCustomerPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetCustomerPagedQueryHandler(IAppDbContext context,
            ILogger<GetCustomerPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<CustomerForListDto>>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetCustomerPagedQueryHandler));

            var query = _context.Customers
            .AsNoTracking()
            .OrderBy(x => x.CustomerName)
            .Select(entity => new CustomerForListDto()
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName,
                CustomerCode = entity.CustomerCode,
                ContactId = entity.ContactId,
                AddressId = entity.AddressId,
                 BuildingNumber = entity.Address!.BuildingNumber!,
                Email = entity.Contact!.Email,
                PhoneNumber = entity.Contact.PhoneNumber,
                Street = entity.Address.Street!,
                
               
            });


            var result = await query.ToListAsync(
                cancellationToken);

            _logger.LogInformation("GetCustomerPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

