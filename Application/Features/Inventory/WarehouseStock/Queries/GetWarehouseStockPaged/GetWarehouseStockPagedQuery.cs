using Contract.Common.Constants;
using Contract.Common.Interfaces;
using Contract.Common.Models;
using Contract.Features.Inventory.WarehouseStock.DTOs;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Inventory.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged
{
    public sealed record GetWarehouseStockPagedQuery(Guid WarehouseId) : ICachedQuery<Result<PaginatedList<WarehouseStockDtoForList>>>
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;
        public string CacheKey => CacheKeys.ForEntityPaged(CacheGroups.Inventory, CacheEntities.WarehouseStock, nameof(GetWarehouseStockPagedQuery) + WarehouseId , PageNumber, PageSize);
        public string[] Tags => [CacheEntities.WarehouseStock] ;
    }
}

