using Contract.Common.Constants;
using Contract.Features.Inventory.WarehouseStocks.Queries.GetWarehouseStockPaged;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Inventory.WarehouseStock.Queries.GetWarehouseStockById
{
    public sealed class GetWarehouseQueryStockByIdValidator
    : AbstractValidator<GetWarehouseStockByIdQuery>
    {
        public GetWarehouseQueryStockByIdValidator()
        {

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Invalid Id Sent.");
        }
    }
}
