using Contract.Requests.Addresses;
using Contract.Requests.Cities;
using Contract.Requests.ContactInfos;
using Contract.Requests.Countries;
using Contract.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.HttpClient;
using static HotelSystemUI.HttpClients.HttpClientHelper;

namespace UI.Services
{
    public static class CountriesServices
    {
        public static async Task<ApiResult<List<CityDto>>> GetCities(Guid countryId)
        {
            var response = await _inventoryClient.GetAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Countries.GetAllCities(countryId));
            return await ReadResponse<List<CityDto>>(response);
        }

        public static async Task<ApiResult<List<CountryDto>>> GetAll()
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Countries.CountriesRoute;
            var response = await _inventoryClient.GetAsync($"{route}");
            return await ReadResponse<List<CountryDto>>(response);
        }

        public static async Task<ApiResult<CountryDto>> Get(Guid countryId)
        {
            var response = await _inventoryClient.GetAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Countries.GetById(countryId));
            return await ReadResponse<CountryDto>(response);
        }

        public static async Task<ApiResult<CountryDto>> Create(CreateCountryRequest request)
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Countries.CountriesRoute;
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(route, content);
            return await ReadResponse<CountryDto>(response);
        }

        public static async Task<ApiResult<CountryDto>> Update(Guid countryId, UpdateCountryRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Countries.GetById(countryId), content);
            return await ReadResponse<CountryDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid countryId)
        {
            var response = await _inventoryClient.DeleteAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Countries.GetById(countryId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }

    public static class CitiesServices
    {
        public static async Task<ApiResult<List<CityDto>>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Cities.CitiesRoute;
            var response = await _inventoryClient.GetAsync($"{route}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<CityDto>>(response);
        }

        public static async Task<ApiResult<CityDto>> Get(Guid cityId)
        {
            var response = await _inventoryClient.GetAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Cities.GetById(cityId));
            return await ReadResponse<CityDto>(response);
        }

        public static async Task<ApiResult<CityDto>> Create(CreateCityRequest request)
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Cities.CitiesRoute;
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(route, content);
            return await ReadResponse<CityDto>(response);
        }

        public static async Task<ApiResult<CityDto>> Update(Guid cityId, UpdateCityRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Cities.GetById(cityId), content);
            return await ReadResponse<CityDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid cityId)
        {
            var response = await _inventoryClient.DeleteAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Cities.GetById(cityId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }

    public static class AddressesServices
    {
        public static async Task<ApiResult<List<AddressDto>>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Addresses.AddressesRoute;
            var response = await _inventoryClient.GetAsync($"{route}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<AddressDto>>(response);
        }

        public static async Task<ApiResult<AddressDto>> Get(Guid addressId)
        {
            var response = await _inventoryClient.GetAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Addresses.GetById(addressId));
            return await ReadResponse<AddressDto>(response);
        }

        public static async Task<ApiResult<AddressDto>> Create(CreateAddressEntryRequest request)
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Addresses.AddressesRoute;
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(route, content);
            return await ReadResponse<AddressDto>(response);
        }

        public static async Task<ApiResult<AddressDto>> Update(Guid addressId, UpdateAddressEntryRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Addresses.GetById(addressId), content);
            return await ReadResponse<AddressDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid addressId)
        {
            var response = await _inventoryClient.DeleteAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.Addresses.GetById(addressId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }

    public static class ContactInfosServices
    {
        public static async Task<ApiResult<List<ContactInfoDto>>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.ContactInfos.ContactInfosRoute;
            var response = await _inventoryClient.GetAsync($"{route}?pageNumber={pageNumber}&pageSize={pageSize}");
            return await ReadResponse<List<ContactInfoDto>>(response);
        }

        public static async Task<ApiResult<ContactInfoDto>> Get(Guid contactInfoId)
        {
            var response = await _inventoryClient.GetAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.ContactInfos.GetById(contactInfoId));
            return await ReadResponse<ContactInfoDto>(response);
        }

        public static async Task<ApiResult<ContactInfoDto>> Create(CreateContactInfoEntryRequest request)
        {
            var route = HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.ContactInfos.ContactInfosRoute;
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PostAsync(route, content);
            return await ReadResponse<ContactInfoDto>(response);
        }

        public static async Task<ApiResult<ContactInfoDto>> Update(Guid contactInfoId, UpdateContactInfoEntryRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _inventoryClient.PutAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.ContactInfos.GetById(contactInfoId), content);
            return await ReadResponse<ContactInfoDto>(response);
        }

        public static async Task<ApiResult<bool>> Delete(Guid contactInfoId)
        {
            var response = await _inventoryClient.DeleteAsync(HotelSystemUI.HttpClients.InventorySystemUI.HttpClients.Routes.Entities.ContactInfos.GetById(contactInfoId));

            if (response.IsSuccessStatusCode)
                return ApiResult<bool>.Success(true);

            return await ReadResponse<bool>(response);
        }
    }
}

