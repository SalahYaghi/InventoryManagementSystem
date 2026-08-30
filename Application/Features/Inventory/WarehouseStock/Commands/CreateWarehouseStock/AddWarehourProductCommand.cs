using Contract.Features.Inventory.Product.Commands.CreateProduct;
using Contract.Features.Inventory.Warehouses.Commands.CreateWarehouse;
using Contract.Features.Inventory.WarehouseStocks.DTOs;
using Inventory.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.WarehouseStock.Commands.AddWarehouseProducts
{
    public class AddWarehourProductCommand : IRequest<Result<WarehouseStockDto>>
    {
        public Guid WarehousesId { get; set; }
        public CreateProductCommand Product { get; set; } = default!;

    }
}

