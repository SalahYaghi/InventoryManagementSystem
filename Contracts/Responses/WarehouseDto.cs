using System;
using Contract.Common;

namespace Contract.Responses
{
    public class WarehouseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid AddressId { get; set; }
        public AddressDto?
            Address { get; set; }
        public WarehouseStatus WarehouseStatus { get; set; }
    }
}


