using Contract.Requests.Orders;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Orders;

namespace UI.Services
{
    public static class OrdersServices
    {
        public static async Task<ApiResult<PaginatedList<OrderForListDto>>> GetAll(int pageNumber = 1, int pageSize = 60,
            OrderType? orderType = null)
        {
            string route = $"{OrdersRoute}?pageNumber={pageNumber}&pageSize={pageSize}";
            if (orderType != null)
                route += $"&orderType={orderType}";

            var response = await _inventoryClient.GetAsync(route);
            return await ReadResponse<PaginatedList<OrderForListDto>>(response);
        }

        public static async Task<ApiResult<OrderDto>> Get(Guid orderId)
        {
            var response = await _inventoryClient.GetAsync(GetById(orderId));
            return await ReadResponse<OrderDto>(response);
        }

        public static async Task<ApiResult<List<OrderDetailForListDto>>> GetOrderDetails(Guid orderId, int pageNumber = 1, int pageSize = 60)
        {
            var response = await _inventoryClient.GetAsync($"{Details(orderId)}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<OrderDetailForListDto>>(response);
        }

        public static async Task<ApiResult<OrderDetailDto>> GetOrderDetail(Guid detailId)
        {
            var response = await _inventoryClient.GetAsync(DetailById(detailId));
            return await ReadResponse<OrderDetailDto>(response);
        }

        public static async Task<ApiResult<OrderDto>> Create(CreateOrderRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(OrdersRoute, content);
            return await ReadResponse<OrderDto>(response);
        }

        public static async Task<ApiResult<bool>> Update(Guid orderId, UpdateOrderRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(orderId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> UpdateStatus(Guid orderId, UpdateOrderStatusRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(Status(orderId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid orderId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(orderId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<OrderDetailDto>> CreateOrderDetail(CreateOrderDetailRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(OrderDetailsRoute, content);
            return await ReadResponse<OrderDetailDto>(response);
        }

        public static async Task<ApiResult<bool>> UpdateOrderDetailQuantity(Guid detailId, UpdateOrderDetailQuantityRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(DetailById(detailId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> DeleteOrderDetail(Guid detailId)
        {
            var response = await _inventoryClient.DeleteAsync(DetailById(detailId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

