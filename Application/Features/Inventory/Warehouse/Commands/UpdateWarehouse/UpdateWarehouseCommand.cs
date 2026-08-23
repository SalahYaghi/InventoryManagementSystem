using MediatR;
using MechanicShop.Domain.Common.Results;
using Contract.Features.Inventory.Warehouses.DTOs;
using Domain.Contacts.Address;
using Contract.Features.References.Addresses.Commands.UpdateAddress;

namespace Contract.Features.Inventory.Warehouses.Commands.UpdateWarehouse
{
    public sealed record UpdateWarehouseCommand : IRequest<Result<WarehouseDto>>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
        public UpdateAddressCommand? Address { get; init; }
        public Domain.Warehouses.WarehouseStatus WarehouseStatus { get; init; }
    }
}

