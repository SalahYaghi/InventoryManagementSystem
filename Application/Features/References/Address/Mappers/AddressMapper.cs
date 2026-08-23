using Domain.Contacts.Address;
using Contract.Features.References.Addresses.DTOs;
using Contract.Features.References.Countries.Mappers;
using Contract.Features.References.Cities.Mappers;

namespace Contract.Features.References.Addresses.Mappers
{
    public static class AddressMapper
    {
        public static AddressDto ToDto(this Domain.Contacts.Address.Address entity)
        {
                return new AddressDto
            {
                Id = entity.Id,
                CountryId = entity.CountryId,
                CityId = entity.CityId,
                PostalCode = entity.PostalCode,
                BuildingNumber = entity.BuildingNumber,
                Street = entity.Street,
                Description = entity.Description , 
                Country = entity?.Country?.ToDto() ,
                City = entity?.City?.ToDto() ,    
            };
        }
    }
}

