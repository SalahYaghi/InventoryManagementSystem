using ContracOldCompatibile.Requests.Employees;
using Contract.Requests.Suppliers;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities;
using static HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Employees;


namespace UI.Shared.Services
{
    public class EmployeeServices
    {


        public static async Task<ApiResult<List<EmployeeDtoForList>>> GetAll(Guid? warehouseId = null)
        {
            string command = $"{EmployeeeRoute}";
            if (warehouseId.HasValue)
                command += $"?warehouseId={warehouseId}";

            var response = await _inventoryClient.GetAsync($"{command}");
            return await ReadResponse<List<EmployeeDtoForList>>(response);
        }

        public static async Task<ApiResult<EmployeeDto>> Get(Guid employeeId)
        {
            var response = await _inventoryClient.GetAsync(GetById(employeeId));
            return await ReadResponse<EmployeeDto>(response);
        }

        public static async Task<ApiResult<EmployeeDto>> Create(CreateEmployeeWithPersonIdRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(EmployeeeRoute, content);
            return await ReadResponse<EmployeeDto>(response);
        }

        public static async Task<ApiResult<SupplierDto>> Update(Guid empId, UpdateEmployeeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(GetById(empId), content);
            return await ReadResponse<SupplierDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid employeeId)
        {
            var response = await _inventoryClient.DeleteAsync(GetById(employeeId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }

    }
}

