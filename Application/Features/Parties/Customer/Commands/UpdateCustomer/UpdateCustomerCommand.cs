using Contract.Features.Parties.Customers.DTOs;
using Contract.Features.References.Addresses.Commands.UpdateAddress;
using Contract.Features.References.ContactInfos.Commands.UpdateContactInfo;
using MechanicShop.Domain.Common.Results;
using MediatR;

namespace Contract.Features.Parties.Customers.Commands.UpdateCustomer
{
    public sealed record UpdateCustomerCommand : IRequest<Result<CustomerDto>>
    {
        public Guid Id { get; init; }
        public string CustomerName { get; init; } = string.Empty;
        public string CustomerCode { get; init; } = string.Empty;
        public UpdateContactInfoCommand? Contact { get; init; } = default!;
        public UpdateAddressCommand? Address { get; init; } = default!;
         public string? Notes { get; init; }
    }
}

