using Contract.Common.Files;
using MechanicShop.Domain.Common.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Interfaces
{
    public interface IFileStorage
    {
         Task<Stream> CompressToZip(FileDto[] urls);
        bool CheckFileExists(string filePath);
        Task<bool> IsImage(IFormFile file);
        Task<string> GetImageExtension(IFormFile image, CancellationToken ct = default);
        string GetMimeType(string path);
        void DeleteFile(string filePath);
        void DeleteFiles(string []filePaths);
        Task<Result<string>> SaveFile(IFormFile file, string filePath , CancellationToken cancellationToken = default);

    }
}

