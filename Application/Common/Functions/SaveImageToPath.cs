using Contract.Common.Interfaces;
using Domain.Common.Helpers; // [FIX 7.2] ValidationHelper lives in Domain.Common.Helpers
using Contract.Features.References.Document;
using MechanicShop.Domain.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Contract.Common.Functions
{
    public static class ImageFunctions
    {
        public static async Task<Result<string>> Save<T>(
            IFormFile? image,
            string path,
            ILogger<T> logger,
            IFileStorage storage,
            CancellationToken ct = default)
        {
            if (image is null || image.Length == 0)
            {
                logger.LogWarning("Attempt to add a null image.");
                return DocumentApplicationErrors.ImageIsRequired;
            }

            if (!await storage.IsImage(image))
            {
                logger.LogWarning("Attempt to save image with an invalid image format.");
                return DocumentApplicationErrors.ImageFormattingError;
            }

            var imageUrlRes = await storage.SaveFile(image, path, ct);

            if (imageUrlRes.IsError)
            {
                logger.LogError("Error saving image: {Errors}", imageUrlRes.Errors);
                return DocumentApplicationErrors.ErrorSavingImage;
            }

            var imageUrl = imageUrlRes.Value;

            if (string.IsNullOrEmpty(imageUrl))
            {
                logger.LogError("File storage returned an empty path for the saved image.");
                return DocumentApplicationErrors.ImageFormattingError;
            }

            if (!ValidationHelper.IsValidImageUrlOrPath(imageUrl))
            {
                logger.LogError(
                    "File storage returned a path the domain will reject (no image extension): {ImageUrl}", imageUrl);

                storage.DeleteFile(imageUrl);
                return DocumentApplicationErrors.ImageFormattingError;
            }

            return imageUrl;
        }
    }
}
