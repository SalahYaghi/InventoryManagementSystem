using Domain.Customer;
using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.References.Addresses.Mappers;
using Contract.Features.References.ContactInfos.Mappers;

namespace Contract.Features.Parties.Customers.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(this Domain.Customer.Customer entity)
        {
            return new CustomerDto
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName,
                CustomerCode = entity.CustomerCode,
                ContactId = entity.ContactId,
                AddressId = entity.AddressId,
                 Notes = entity.Notes , 
                Address = entity.Address?.ToDto() ,  
                Contact = entity.Contact?.ToDto()
            };
        }
    }
}

