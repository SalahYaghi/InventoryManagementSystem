using Contract.Features.User.Commands.CreateUser;
using Contract.Features.User.Dtos;
using ContracOldCompatibile.Requests.Users;
using Contract.Requests.Suppliers;
using Contract.Requests.Users;
using Contract.Responses;
using Newtonsoft.Json;
using OldContract.Features.User.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.User;


namespace UI.Shared.Services
{
    public static class UserServices
    {
        public static async Task<ApiResult<List<UserForListDto>>> GetAll( )
        {
            var response = await _inventoryClient.GetAsync($"{UserRoute}");
            return await ReadResponse<List<UserForListDto>>(response);
        }

        public static async Task<ApiResult<UserDto>> Get(Guid userId)
        {
            var response = await _inventoryClient.GetAsync(GetById(userId));
            return await ReadResponse<UserDto>(response);
        }
        public static async Task<ApiResult<UserDto>> GetByEmail(string email)
        {
            var response = await _inventoryClient.GetAsync(User.GetByEmail(email));
            return await ReadResponse<UserDto>(response);
        }

        public static async Task<ApiResult<UserDto>> Create(CreateUserRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(UserRoute, content);
            return await ReadResponse<UserDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid userId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(userId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

        public static async Task<ApiResult<bool>> UpdateUserPassword(Guid id , UpdateUserPasswordRequest request) {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(UpdatePassword(id), content);
            return await ReadResponse<bool>(response);

        }

        public static async Task<ApiResult<UserDto>> Update(Guid supplierId, UpdateUserRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(supplierId), content);
            return await ReadResponse<UserDto>(response);
        }

    }
}

