using Contracts.Requests.Customers;
using Contracts.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Customers;

namespace UI.Services
{
    public static class CustomersServices
    {
        public static async Task<ApiResult<List<CustomerDto>>> GetAll(int pageNumber = 1, int pageSize = 60)
        {
            var response = await _inventoryClient.GetAsync($"{CustomersRoute}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<CustomerDto>>(response);
        }

        public static async Task<ApiResult<CustomerDto>> Get(Guid customerId)
        {
            var response = await _inventoryClient.GetAsync(GetById(customerId));
            return await ReadResponse<CustomerDto>(response);
        }

        public static async Task<ApiResult<CustomerDto>> Create(CreateCustomerRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(CustomersRoute, content);
            return await ReadResponse<CustomerDto>(response);
        }

        public static async Task<ApiResult<CustomerDto>> Update(Guid customerId, UpdateCustomerRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(customerId), content);
            return await ReadResponse<CustomerDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid customerId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(customerId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

