using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged;
using Domain.Warehouses;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById
{
    public sealed record GetWarehouseStockByIdQuery(Guid Id) : ICachedQuery<Result<WarehouseStockDto>>
    {
        public string CacheKey => CacheKeys.ForEntityById(CacheGroups.Inventory, CacheEntities.WarehouseStock, nameof(GetWarehouseStockByIdQuery)  ,  Id);
        public string[] Tags => [CacheEntities.WarehouseStock];
    }
}
