using Contract.Features.Parties.Supplier.DTOs;
using Contract.Features.References.Addresses.Mappers;
using Contract.Features.References.ContactInfos.Mappers;
using Domain.Suppliers;

namespace Contract.Features.Parties.Supplier.Mappers
{
    public static class SupplierMapper
    {
        public static SupplierDto ToDto(this Domain.Suppliers.Supplier entity)
        {
            return new SupplierDto
            {
                Id = entity.Id,
                SupplierName = entity.SupplierName,
                SupplierCode = entity.SupplierCode,
                ContactId = entity.ContactId,
                AddressId = entity.AddressId,
                Status = entity.Status,
                Notes = entity.Notes,
                Address = entity.Address?.ToDto() ,
                Contact = entity.Contact?.ToDto()
            };
        }
    }
}

