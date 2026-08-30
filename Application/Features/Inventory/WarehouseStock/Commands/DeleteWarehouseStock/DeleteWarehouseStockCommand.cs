using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.Inventory.WarehouseStocks.Commands.DeleteWarehouseStock
{
    public sealed record DeleteWarehouseStockCommand(Guid Id) : IRequest<Result<Deleted>>;
}

