using Contract.Common.Files;
using Contract.Common.Interfaces;
using Contract.Features.Inventory.Product.DTOs;
using MechanicShop.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Contract.Features.Inventory.Product.Queries.GetAllProductImages
{
    public class GetAllProductImagesQueryHandler(IAppDbContext context , 
        IFileStorage fileStorage,
        ILogger<GetAllProductImagesQueryHandler> logger) : IRequestHandler<GetAllProductImagesQuery, Result<FileDto>>
    {
        private readonly ILogger<GetAllProductImagesQueryHandler> _logger = logger;

        public async Task<Result<FileDto>> Handle
            (GetAllProductImagesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started handling {RequestName}.", nameof(GetAllProductImagesQueryHandler));

            var entity = await context.Products
                .Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity is null)

            {

                _logger.LogWarning("GetAllProductImagesQueryHandler stopped because an error result was returned: {ErrorResult}.", "Error.NotFound(\"Product.NotFound\", \"Product was not found.\")");
                return Error.NotFound("Product.NotFound", "Product was not found.");

            }

            string contentType = "application/zip";
            string fileName = $"{entity.Id}-products-images"; 
            Stream memoryStream = await fileStorage.CompressToZip(entity.ProductImages.Select(img => new FileDto() { FileUrl = img.ImageUrl , FileName = img.Id.ToString() }).ToArray()); 

            return new FileDto() { 
                FileName = fileName,
                ContentType = contentType,
                Stream = memoryStream
            };
        }
    }
}

