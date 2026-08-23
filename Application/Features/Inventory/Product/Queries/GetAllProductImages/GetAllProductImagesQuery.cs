using Contract.Common.Files;
using Contract.Features.Inventory.Product.DTOs;
using MechanicShop.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.Product.Queries.GetAllProductImages
{
    public sealed record GetAllProductImagesQuery(Guid Id) : IRequest<Result<FileDto>>;
    
}

