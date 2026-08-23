using Contract.Requests.Products;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using System.Net.Http.Json;

using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Products;
using UI.Shared.Helpers.IO_Helper;
using System.Linq;
using UI.Shared.MemoryData;

namespace UI.Services
{
    public static class ProductsServices
    {
        public static async Task<ApiResult<PaginatedList<ProductDtoForList>>> GetAll(int pageNumber = 1, int pageSize = 60, Guid? excludeSupplierId = null,  
         List<Guid> excludeProductsIds = null,
         Guid? fromWarehouseId = null,
         Guid? fromSupplierId = null)
        {
            string route = $"{ProductsRoute}?pageNumber={pageNumber}&pageSize={pageSize}";

            if (excludeSupplierId.HasValue && excludeSupplierId.Value != Guid.Empty)
            {
                route += $"&excludeSupplierId={excludeSupplierId}";
            }

            if (fromWarehouseId.HasValue && fromWarehouseId.Value != Guid.Empty)
            {
                route += $"&fromWarehouseId={fromWarehouseId}";
            }

            if (fromSupplierId.HasValue && fromSupplierId.Value != Guid.Empty)
            {
                route += $"&fromSupplierId={fromSupplierId}";
            }

            if (excludeProductsIds != null && excludeProductsIds.Count > 0)
            {
                route += string.Concat(excludeProductsIds.Select(i => $"&excludeProductsIds={i}"));
            }

            var response = await _inventoryClient.GetAsync(route);

         

            return await ReadResponse<PaginatedList<ProductDtoForList>>(response);
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

        public static async Task<ApiResult<List<FileResponse>>> GetProductImages(Guid productId)
        {
            var response = await _inventoryClient.GetAsync(Images(productId));

            if (!response.IsSuccessStatusCode)
                return "Failed to load images"; 

            var zip = await response.Content.ReadAsByteArrayAsync();

            return await FileHelper.DecompressToZip(zip);
        }

        public static async Task<ApiResult<bool>> DeleteProductImage(Guid imageId)
        {
            var response = await _inventoryClient.DeleteAsync(Image(imageId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

