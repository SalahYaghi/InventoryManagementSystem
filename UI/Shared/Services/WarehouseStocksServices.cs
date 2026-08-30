using Contract.Requests.Warehouses;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.WarehouseStocks;

namespace UI.Services
{
    public static class WarehouseStocksServices
    {
        public static async Task<ApiResult<PaginatedList<WarehouseStockDtoForList>>> GetByWarehouse(Guid warehouseId, int pageNumber = 1, int pageSize = 60)
        {
            
            var response = await _inventoryClient.GetAsync($"{ByWarehouse(warehouseId)}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<PaginatedList<WarehouseStockDtoForList>>(response);
        }


        public static async Task<ApiResult<WarehouseStockDto>> GetByWarehouseStockById(Guid id)
        {

            var response = await _inventoryClient.GetAsync($"{ByIdWarehouseStock(id)}");
            return await ReadResponse<WarehouseStockDto>(response);
        }



        public static async Task<ApiResult<bool>> AddProduct(AddWarehouseProductRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(WarehouseStocksRoute, content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> UpdateMinimumLevel(Guid warehouseStockId, UpdateWarehouseStockMinimumLevelRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(MinimumLevel(warehouseStockId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid warehouseStockId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(warehouseStockId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

