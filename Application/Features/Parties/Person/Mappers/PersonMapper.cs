using Contract.Features.Parties.People.DTOs;
using Contract.Features.Parties.Person.DTOs;
using Contract.Features.References.Addresses.Mappers;
using Contract.Features.References.ContactInfos.Mappers;
using Contract.Features.References.Documents.Mappers;
using Domain.People;

namespace Contract.Features.Parties.People.Mappers
{
    public static class PersonMapper
    {
        public static PersonDto ToDto(this Domain.People.Person entity)
        {
            return new PersonDto
            {
                Id = entity.Id,
                NationalNo = entity.NationalNo,
                FirstName = entity.FirstName,
                SecondName = entity.SecondName,
                ThirdName = entity.ThirdName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                DateOfBirth = entity.DateOfBirth,
                ContactId = entity.ContactId,
                AddressId = entity.AddressId,
                DocumentId = entity.DocumentId , 
                Address = entity?.Address?.ToDto()  ,
                Contact = entity?.Contact?.ToDto(),
                Document = entity?.Document?.ToDto()
          
            };
        }

       
    }
}

