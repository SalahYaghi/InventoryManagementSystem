using Contracts.Requests.Adjustment;
using Contracts.Requests.Adjustments;
using Contracts.Requests.Orders;
using Contracts.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Adjustments;

namespace UI.Services
{
    public static class AdjustmentsServices
    {
        public static async Task<ApiResult<List<AdjustmentForListDto>>> GetAll(int pageNumber = 1, int pageSize = 60)
        {
            var response = await _inventoryClient.GetAsync($"{AdjustmentsRoute}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<AdjustmentForListDto>>(response);
        }

        public static async Task<ApiResult<AdjustmentDto>> Get(Guid adjustmentId)
        {
            var response = await _inventoryClient.GetAsync(GetById(adjustmentId));
            return await ReadResponse<AdjustmentDto>(response);
        }

        public static async Task<ApiResult<List<AdjustmentDetailForListDto>>> GetAdjustmentDetails(Guid adjustmentId, int pageNumber = 1, int pageSize = 60)
        {
            var response = await _inventoryClient.GetAsync($"{Details(adjustmentId)}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<AdjustmentDetailForListDto>>(response);
        }

        public static async Task<ApiResult<AdjustmentDetailDto>> GetAdjustmentDetail(Guid detailId)
        {
            var response = await _inventoryClient.GetAsync(DetailById(detailId));
            return await ReadResponse<AdjustmentDetailDto>(response);
        }

        public static async Task<ApiResult<AdjustmentDto>> Create(CreateAdjustmentRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(AdjustmentsRoute, content);
            return await ReadResponse<AdjustmentDto>(response);
        }

        public static async Task<ApiResult<bool>> Update(Guid adjustmentId, UpdateAdjustmentRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(adjustmentId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> UpdateStatus(Guid adjustmentId, UpdateAdjustmentStatusRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(Status(adjustmentId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid adjustmentId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(adjustmentId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<AdjustmentDetailDto>> CreateAdjustmentDetail(CreateAdjustmentDetailRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(AdjustmentDetailsRoute, content);
            return await ReadResponse<AdjustmentDetailDto>(response);
        }

        public static async Task<ApiResult<bool>> UpdateAdjustmentDetailQuantity(Guid detailId, UpdateAdjustmentDetailQuantityRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(DetailById(detailId), content);

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> DeleteAdjustmentDetail(Guid detailId)
        {
            var response = await _inventoryClient.DeleteAsync(DetailById(detailId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

