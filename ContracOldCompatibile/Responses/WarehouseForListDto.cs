using System;
namespace Contract.Responses
{
    public class WarehouseForListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public bool IsActived { get; set; }
    }
}



