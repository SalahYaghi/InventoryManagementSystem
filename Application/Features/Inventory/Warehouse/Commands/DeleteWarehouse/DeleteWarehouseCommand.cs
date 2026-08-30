using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Inventory.Warehouses.Commands.DeleteWarehouse
{
    public sealed record DeleteWarehouseCommand(Guid Id) : IRequest<Result<Deleted>>;
}

