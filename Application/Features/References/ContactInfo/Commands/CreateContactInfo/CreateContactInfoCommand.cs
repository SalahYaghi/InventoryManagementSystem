using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.References.ContactInfos.DTOs;

namespace Contract.Features.References.ContactInfos.Commands.CreateContactInfo
{
    public sealed record CreateContactInfoCommand : IRequest<Result<ContactInfoDto>>
    {
         public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string AlternitavePhoneNumber { get; init; } = string.Empty;
        public string? FaxNumber { get; init; }
        public string? WebsiteUrl { get; init; }
    }
}

