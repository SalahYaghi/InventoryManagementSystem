using Contract.Features.Transactions.Order.DTOs;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Transactions.Order.Commands.CreateOrderDetail
{
   
    public sealed record CreateOrderDetailCommand : IRequest<Result<OrderDetailDto>>
    {
        public byte[] RowVersion { get; set; } = [];

        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public decimal Quantity { get; init; } 
    }
}

