using Contract.Common.Extensions;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.Parties.Supplier.Mappers;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Parties.Supplier.Queries.GetSupplierPaged
{
    public sealed class GetSupplierPagedQueryHandler : IRequestHandler<GetSupplierPagedQuery, Result<List<SupplierForListDto>>>
    {
        private readonly ILogger<GetSupplierPagedQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetSupplierPagedQueryHandler(IAppDbContext context,
            ILogger<GetSupplierPagedQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<List<SupplierForListDto>>> Handle(GetSupplierPagedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetSupplierPagedQueryHandler));

          
            var query = _context.Suppliers
                .AsNoTracking()
                .OrderBy(x => x.SupplierName)
                .Select(entity => new SupplierForListDto() {
                    Id = entity.Id,
                    SupplierName = entity.SupplierName,
                    SupplierCode = entity.SupplierCode,
                    ContactId = entity.ContactId,
                    AddressId = entity.AddressId,
                    Status = entity.Status,
                    BuildingNumber = entity.Address!.BuildingNumber ,
                    Email = entity.Contact!.Email,
                    PhoneNumber = entity.Contact.PhoneNumber,
                    Street = entity.Address.Street,
                });


            var result = await query.ToListAsync(
                cancellationToken);

            _logger.LogInformation("GetSupplierPagedQueryHandler completed successfully.");
            return result;
        }
    }
}

