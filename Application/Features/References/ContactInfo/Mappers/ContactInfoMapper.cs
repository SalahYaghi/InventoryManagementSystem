using Domain.Contacts.ContactInfo;
using Contract.Features.References.ContactInfos.DTOs;

namespace Contract.Features.References.ContactInfos.Mappers
{
    public static class ContactInfoMapper
    {
        public static ContactInfoDto ToDto(this Domain.Contacts.ContactInfo.ContactInfo entity)
        {
            return new ContactInfoDto
            {
                Id = entity.Id,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                AlternitavePhoneNumber = entity.AlternitavePhoneNumber,
                FaxNumber = entity.FaxNumber,
                WebsiteUrl = entity.WebsiteUrl , 
            };
        }
    }
}

