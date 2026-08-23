using Contract.Features.Inventory.Warehouse.DTOs;
using Contract.Features.Inventory.Warehouses.DTOs;
using Contract.Features.References.Addresses.Mappers;
using Domain.Warehouses;

namespace Contract.Features.Inventory.Warehouses.Mappers
{
    public static class WarehouseMapper
    {
        public static WarehouseForListDto ToDtoForList(this Domain.Warehouses.Warehouse entity)
        {
            return new WarehouseForListDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                IsActived = entity.WarehouseStatus == Domain.Warehouses.WarehouseStatus.Active ,
                BuildingNumber = entity.Address?.BuildingNumber ?? string.Empty,
                Street = entity.Address?.Street ?? string.Empty,
                
            };
        
    }
        public static WarehouseDto ToDto(this Domain.Warehouses.Warehouse entity)
        {
            return new WarehouseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                AddressId = entity.AddressId,
                WarehouseStatus = entity.WarehouseStatus,
                Address = entity.Address?.ToDto()
               
            };
        }
    }
}

