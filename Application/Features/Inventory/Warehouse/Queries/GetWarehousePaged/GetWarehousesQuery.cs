using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.Warehouse.DTOs;
using Contract.Features.Inventory.Warehouses.DTOs;
using MechanicShop.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Inventory.Warehouses.Queries.GetWarehousePaged
{
    public sealed record GetWarehousesQuery : ICachedQuery<Result<List<WarehouseForListDto>>>
    {
        public string CacheKey => CacheKeys.ForEntityList(CacheGroups.Inventory, CacheEntities.Warehouse, nameof(GetWarehousesQuery));
        public string[] Tags => [CacheEntities.Warehouse];
    }
}

