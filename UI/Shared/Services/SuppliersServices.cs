using Contract.Requests.Suppliers;
using Contract.Requests.SupplierProducts;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Suppliers;

namespace UI.Services
{
    public static class SuppliersServices
    {
        public static async Task<ApiResult<List<SupplierForListDto>>> GetAll()
        {
            var response = await _inventoryClient.GetAsync($"{SuppliersRoute}");
            return await ReadResponse<List<SupplierForListDto>>(response);
        }

        public static async Task<ApiResult<SupplierDto>> Get(Guid supplierId)
        {
            var response = await _inventoryClient.GetAsync(GetById(supplierId));
            return await ReadResponse<SupplierDto>(response);
        }

        public static async Task<ApiResult<SupplierDto>> Create(CreateSupplierRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(SuppliersRoute, content);
            return await ReadResponse<SupplierDto>(response);
        }

        public static async Task<ApiResult<SupplierDto>> Update(Guid supplierId, UpdateSupplierRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(supplierId), content);
            return await ReadResponse<SupplierDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid supplierId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(supplierId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<List<SupplierProductDtoForList>>> GetSupplierProducts(Guid supplierId)
        {
            var response = await _inventoryClient.GetAsync(Products(supplierId));
            return await ReadResponse<List<SupplierProductDtoForList>>(response);
        }

        public static async Task<ApiResult<SupplierProductDto>> GetSupplierProduct(Guid supplierId, Guid productId)
        {
            var response = await _inventoryClient.GetAsync(Product(supplierId, productId));
            return await ReadResponse<SupplierProductDto>(response);
        }

        public static async Task<ApiResult<SupplierProductDto>> CreateSupplierProduct(Guid supplierId, CreateSupplierProductRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(Products(supplierId), content);
            return await ReadResponse<SupplierProductDto>(response);
        }

        public static async Task<ApiResult<bool>> UpdateSupplierProduct(Guid supplierId, Guid productId, UpdateSupplierProductRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(Product(supplierId, productId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> DeleteSupplierProduct(Guid supplierId, Guid productId)
        {
            var response = await _inventoryClient.DeleteAsync(Product(supplierId, productId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

