using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.Product.Commands.CreateProductImage
{
    public sealed record CreateProductImageCommand(Guid ProductId ,IFormFile Image) : IRequest<Result<Created>>
    {
    }
}

