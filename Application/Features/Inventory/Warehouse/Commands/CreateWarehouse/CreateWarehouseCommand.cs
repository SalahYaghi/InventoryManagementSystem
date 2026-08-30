using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Inventory.Warehouses.DTOs;
using Domain.Contacts.Address;
using Contract.Features.References.Addresses.Commands.CreateAddress;

namespace Contract.Features.Inventory.Warehouses.Commands.CreateWarehouse
{
    public sealed record CreateWarehouseCommand : IRequest<Result<WarehouseDto>>
    {
         public string Name { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
        public CreateAddressCommand Address { get; init; } = default!;
     }
}

