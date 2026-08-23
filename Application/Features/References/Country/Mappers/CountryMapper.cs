using Domain.Contacts.Address.Country;
using Contract.Features.References.Countries.DTOs;

namespace Contract.Features.References.Countries.Mappers
{
    public static class CountryMapper
    {
        public static CountryDto ToDto(this Domain.Contacts.Address.Country.Country entity)
        {
            return new CountryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}

