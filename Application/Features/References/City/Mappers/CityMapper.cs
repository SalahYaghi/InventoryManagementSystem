using Domain.Contacts.Address.Country;
using Contract.Features.References.Cities.DTOs;

namespace Contract.Features.References.Cities.Mappers
{
    public static class CityMapper
    {
        public static CityDto ToDto(this Domain.Contacts.Address.Country.City entity)
        {
            return new CityDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}

