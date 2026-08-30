using MediatR;
using Inventory.Domain.Common.Results;
using Contract.Features.Parties.SupplierProducts.DTOs;

namespace Contract.Features.Parties.SupplierProducts.Commands.UpdateSupplierProduct
{
    public sealed record UpdateSupplierProductCommand : IRequest<Result<SupplierProductDto>>
    {
        public Guid SupplierId { get; init; }
        public Guid ProductId { get; init; }

        public decimal PurchasePrice { get; init; }
        public bool IsActive { get; init; }
    }
}

