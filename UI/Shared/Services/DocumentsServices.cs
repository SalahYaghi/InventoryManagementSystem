using Contract.Requests.Documents;
using Contract.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Documents;

namespace UI.Services
{
    public static class DocumentsServices
    {
        public static async Task<ApiResult<List<DocumentDto>>> GetAll(int pageNumber = 1, int pageSize = 60)
        {
            var response = await _inventoryClient.GetAsync($"{DocumentsRoute}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<DocumentDto>>(response);
        }

        public static async Task<ApiResult<DocumentDto>> Get(Guid documentId)
        {
            var response = await _inventoryClient.GetAsync(GetById(documentId));
            return await ReadResponse<DocumentDto>(response);
        }

        public static async Task<ApiResult<DocumentDto>> Create(DocumentType documentType, byte[] imageBytes)
        {
            var form = new MultipartFormDataContent();

            form.Add(new StringContent(((int)documentType).ToString()), "DocumentType");

            if (imageBytes != null && imageBytes.Length > 0)
            {
                var imageContent = new ByteArrayContent(imageBytes);
                form.Add(imageContent, "DocumentImage", "document-image");
            }

            var response = await _inventoryClient.PostAsync(DocumentsRoute, form);
            return await ReadResponse<DocumentDto>(response);
        }

        public static async Task<ApiResult<DocumentDto>> Update(UpdateDocumentRequest request)
        {
            var form = new MultipartFormDataContent();

            form.Add(new StringContent(((int)request.DocumentType).ToString()), "DocumentType");

            if (request.Image != null && request.Image.Length > 0)
            {
                var imageContent = new ByteArrayContent(request.Image);
                form.Add(imageContent, "Image", "document-image");
            }

            var response = await _inventoryClient.PutAsync(GetById(request.Id), form);
            return await ReadResponse<DocumentDto>(response);
        }

        public static async Task<byte[]> GetDocumentImage(Guid documentId)
        {
            var response = await _inventoryClient.GetAsync(Image(documentId));

            if (!response.IsSuccessStatusCode)
                return new byte[0];

            return await response.Content.ReadAsByteArrayAsync();
        }
        public static async Task<ApiResult<bool>> Delete(Guid documentId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(documentId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

