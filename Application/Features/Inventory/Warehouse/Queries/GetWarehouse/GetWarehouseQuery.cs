using Contract.Common.Constants;
using Contract.Common.Interfaces;
using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Warehouses.DTOs;

namespace Contract.Features.Inventory.Warehouses.Queries.GetWarehouse
{
    public sealed record GetWarehouseQuery(Guid Id) : ICachedQuery<Result<WarehouseDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Inventory, CacheEntities.Warehouse, nameof(GetWarehouseQuery), Id);
        public string[] Tags => [CacheEntities.Warehouse];
    }
}

