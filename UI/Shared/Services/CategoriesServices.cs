using Contract.Requests.Categories;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Categories;

namespace UI.Services
{
    public static class CategoriesServices
    {
        public static async Task<ApiResult<List<CategoryDto>>> GetAll()
        {
            var response = await _inventoryClient.GetAsync($"{CategoriesRoute}");
            return await ReadResponse<List<CategoryDto>>(response);
        }

        public static async Task<ApiResult<CategoryDto>> Get(Guid categoryId)
        {
            var response = await _inventoryClient.GetAsync(GetById(categoryId));
            return await ReadResponse<CategoryDto>(response);
        }

        public static async Task<ApiResult<CategoryDto>> Create(CreateCategoryRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(CategoriesRoute, content);
            return await ReadResponse<CategoryDto>(response);
        }

        public static async Task<ApiResult<CategoryDto>> Update(Guid categoryId, UpdateCategoryRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(categoryId), content);
            return await ReadResponse<CategoryDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid categoryId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(categoryId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

