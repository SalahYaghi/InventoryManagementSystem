using Contract.Common.Interfaces;
using Contract.Features.Inventory.Product.DTOs;
using Contract.Features.Inventory.Product.Queries.GetProduct;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Contract.Features.Inventory.WarehouseStocks.Mappers;
using Inventory.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById
{
    public sealed class GetWarehouseStockByIdQueryHandler : IRequestHandler<GetWarehouseStockByIdQuery , Result<WarehouseStockDto>>
    {
        private readonly ILogger<GetWarehouseStockByIdQueryHandler> _logger;

        private readonly IAppDbContext _context;

        public GetWarehouseStockByIdQueryHandler(IAppDbContext context,
            ILogger<GetWarehouseStockByIdQueryHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Result<WarehouseStockDto>> Handle(GetWarehouseStockByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetProductQueryHandler));

            var entity = await _context.WarehouseStocks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("GetWarehouseStockByIdQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Product.NotFound\", \"Warehouse Stock was not found.\")");
                return Error.NotFound("Product.NotFound", "WarehouseStock was not found.");
            }

            _logger.LogInformation("GetWarehouseStockByIdQueryHandler completed successfully.");
            return entity.ToDto();
        }

    }
}
