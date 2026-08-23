using Contract.Features.References.Addresses.DTOs;
using Domain.Contacts.Address;

namespace Contract.Features.Inventory.Warehouses.DTOs
{
    public sealed record WarehouseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
        public Guid AddressId { get; init; }
        public AddressDto? Address { get; init; } 
        public Domain.Warehouses.WarehouseStatus WarehouseStatus { get; init; }
    }
  
}

