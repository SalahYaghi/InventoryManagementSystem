using Contracts.Requests.Warehouses;
using Contracts.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Warehouses;

namespace UI.Services
{
    public static class WarehousesServices
    {
        public static async Task<ApiResult<List<WarehouseForListDto>>> GetAll(int pageNumber = 1, int pageSize = 60)
        {
            var response = await _inventoryClient.GetAsync($"{WarehousesRoute}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<WarehouseForListDto>>(response);
        }

        public static async Task<ApiResult<WarehouseDto>> Get(Guid warehouseId)
        {
            var response = await _inventoryClient.GetAsync(GetById(warehouseId));
            return await ReadResponse<WarehouseDto>(response);
        }

        public static async Task<ApiResult<WarehouseDto>> Create(CreateWarehouseRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(WarehousesRoute, content);
            return await ReadResponse<WarehouseDto>(response);
        }

        public static async Task<ApiResult<WarehouseDto>> Update(Guid warehouseId, UpdateWarehouseRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(warehouseId), content);
            return await ReadResponse<WarehouseDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid warehouseId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(warehouseId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

