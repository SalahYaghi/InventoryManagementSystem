using Contracts.Requests.Products;
using Contracts.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Products;

namespace UI.Services
{
    public static class ProductsServices
    {
        public static async Task<ApiResult<List<ProductDtoForList>>> GetAll(int pageNumber = 1, int pageSize = 60)
        {
            var response = await _inventoryClient.GetAsync($"{ProductsRoute}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<ProductDtoForList>>(response);
        }

        public static async Task<ApiResult<ProductDto>> Get(Guid productId)
        {
            var response = await _inventoryClient.GetAsync(GetById(productId));
            return await ReadResponse<ProductDto>(response);
        }

        public static async Task<ApiResult<ProductDto>> Create(CreateProductRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(ProductsRoute, content);
            return await ReadResponse<ProductDto>(response);
        }

        public static async Task<ApiResult<ProductDto>> Update(Guid productId, UpdateProductRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(productId), content);
            return await ReadResponse<ProductDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid productId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(productId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> CreateProductImage(Guid productId, byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return ApiResult<bool>.Failure("Image is required.");

            var form = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(imageBytes);

            form.Add(imageContent, "Image", "product-image");

            var response = await _inventoryClient.PostAsync(Image(productId), form);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<byte[]> GetProductImages(Guid productId)
        {
            var response = await _inventoryClient.GetAsync(Images(productId));

            if (!response.IsSuccessStatusCode)
                return new byte[0];

            return await response.Content.ReadAsByteArrayAsync();
        }

        public static async Task<ApiResult<bool>> DeleteProductImage(Guid productId)
        {
            var response = await _inventoryClient.DeleteAsync(Image(productId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

