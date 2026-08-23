using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.References.Addresses.Commands.CreateAddress;
using Contract.Features.References.ContactInfos.Commands.CreateContactInfo;
using MechanicShop.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Parties.Customers.Commands.CreateCustomer
{
    public sealed record CreateCustomerCommand : IRequest<Result<CustomerDto>>
    {
         public string CustomerName { get; init; } = string.Empty;
        public string CustomerCode { get; init; } = string.Empty;
        public CreateContactInfoCommand Contact { get; init; } = default!;
        public CreateAddressCommand Address { get; init; } = default!;
         public string? Notes { get; init; }
    }
}

