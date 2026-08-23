using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using UI.HttpClient;
using UI.Shared.HttpClient;
using UI.Shared.Services.Models;

namespace HotelSystemUI.HttpClients
{
    public static class HttpClientHelper
    {

        public static readonly
             HttpClient _inventoryClient;

         
        static HttpClientHelper()
        {
            var handler = new AuthorizationHandler()
            {
                InnerHandler = new HttpClientHandler()
            };

            _inventoryClient = new HttpClient(handler
                )
             {
                 BaseAddress = new Uri(ConfigurationManager.AppSettings["inventoryApiUrl"]),
                
             };

            _inventoryClient.DefaultRequestHeaders.Add(
        HttpClientHelper.TimeZoneHeader,
        TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).ToString());

        }
        public static readonly string TimeZoneHeader = "X-User-Offset";

        public static async Task<ApiResult<T>> ReadResponse<T>(HttpResponseMessage response)
        {
            string body = await response.Content?.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.NotModified)
            {
                return ApiResult<T>.NotModified();
            }

            if (response.IsSuccessStatusCode)
            {
                if (string.IsNullOrWhiteSpace(body))
                    return ApiResult<T>.Success(default(T));

                T data = JsonConvert.DeserializeObject<T>(body);

                return ApiResult<T>.Success(data);
            }


            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(body);

          //  var apiResult = JsonConvert.DeserializeObject<ApiResult<T>>(body);

            return ApiResult<T>.Failure(
                message: problemDetails.Title,
                detail: problemDetails.Title,
                statusCode: (int)response.StatusCode , 
                problemDetails.Errors
            );
        }
      

    }
}

