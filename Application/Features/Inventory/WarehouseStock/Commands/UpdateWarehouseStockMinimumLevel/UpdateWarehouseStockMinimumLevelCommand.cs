using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.WarehouseStocks.DTOs;

namespace Contract.Features.Inventory.WarehouseStocks.Commands.UpdateWarehouseStock
{
    public sealed record UpdateWarehouseStockMinimumLevelCommand : IRequest<Result<WarehouseStockDto>>
    {
        public Guid Id { get; init; }
        public decimal MinimumStockLevel { get; init; }
    }
}

