using Contract.Common.Files;
using Contract.Common.Interfaces;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;

namespace HotelManagementSystemAPI.Helpers
{
    public  class FilesHelper : IFileStorage
    {

        public  async Task<bool> IsImage(IFormFile file)
        {

            using var stream = file.OpenReadStream();
            byte[] header = new byte[8];

            int numberOfBytes = await stream.ReadAsync(header, 0, 8);

            if (numberOfBytes < 3)
                return false;

            if (IsPng(header)
                || IsJpg(header))
                return true;
            return false;

        }

        public  bool IsPng(byte[] bytes)
        {
            byte[] pngHeader = { 0x89, 0x50,
                0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

            return bytes.Take(8).SequenceEqual(pngHeader);
        }

        public  bool IsJpg(byte[] bytes)
        {

            byte[] jpegHeader = { 0xFF, 0xD8, 0xFF };

            return bytes.Take(3).SequenceEqual(jpegHeader);
        }

        public string GetMimeType(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return "application/octet-stream";

            var provider = new  FileExtensionContentTypeProvider();

            if (provider.TryGetContentType(path , out string ? contentType) ) {

                return contentType;
            }
            return "application/octet-stream";
        }

        public  async Task<Result<string>> SaveFile(IFormFile file,
            string directory , CancellationToken cancellationToken = default)
        {

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
          
            string extension = Path.GetExtension(file.FileName);

            if (string.IsNullOrEmpty(extension)) {

                extension = await GetImageExtension(file , cancellationToken); 
            }

            string path = Path.Combine(directory , 
                $"{Guid.NewGuid()}{extension}");

            try
            {
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }catch (Exception ex)
            {
                return Error.Failure("FileSaveError", $"An error occurred while saving the file: {ex.Message}");
            }
            return path;
        }

        public void DeleteFiles(string [] filePath)
        {
            foreach (var path in filePath)
                DeleteFile(path);
        }

        public async Task<string> GetImageExtension(IFormFile image, CancellationToken ct = default)
        {

            using var stream = image.OpenReadStream();
            byte[] header = new byte[8];

            int numberOfBytes = await stream.ReadAsync(header, 0, 8, ct);

            if (numberOfBytes < 3)
                return string.Empty;

            if (IsPng(header))
                return ".png";

            if (IsJpg(header))
                return ".jpg";

            return string.Empty;
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

 
        public async Task<Stream> CompressToZip(FileDto[] urls)
        {
            var stream = new MemoryStream();

            using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true))
            {
                foreach (var img in urls)
                {
                    if (!CheckFileExists(img.FileUrl!))
                        continue;

                    var entry = archive.CreateEntry(
                        Path.GetFileName(img.FileName!),
                        CompressionLevel.Fastest);

                    await using var entryStream = entry.Open();
                    await using var fileStream = File.OpenRead(img.FileUrl!);

                    await fileStream.CopyToAsync(entryStream);
                }
            }  
            stream.Position = 0;
            return stream;
        }

        public bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}

