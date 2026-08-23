using Domain.Contacts.Address;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Contract.Features.Inventory.Warehouse.DTOs
{
    public class WarehouseForListDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public bool IsActived { get; set; }
    }
}

