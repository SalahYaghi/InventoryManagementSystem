using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UI.HttpClient;
using UI.Shared.HttpClient;
using UI.Shared.Services.Models;
using UI.Shared.Storage;

namespace HotelSystemUI.HttpClients
{
    public static class HttpClientHelper
    {
        public static readonly string TimeZoneHeader = "X-User-Offset";

        public static readonly HttpClient _inventoryClient;

        static HttpClientHelper()
        {
            AuthorizationHandler handler = new AuthorizationHandler()
            {
                InnerHandler = new HttpClientHandler()
            };

            _inventoryClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(ConfigurationManagement.GetInventoryApiUrl())
            };

            _inventoryClient.DefaultRequestHeaders.Add(
                TimeZoneHeader,
                TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).ToString());
        }

        public static async Task<ApiResult<T>> ReadResponse<T>(HttpResponseMessage response)
        {
            if (response == null)
                return ApiResult<T>.Failure("No response was received from the server.");

            using (response)
            {
                if (response.StatusCode == HttpStatusCode.NotModified)
                    return ApiResult<T>.NotModified();

                string body = response.Content == null
                    ? string.Empty
                    : await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return ReadSuccess<T>(body);

                return ReadFailure<T>(body, response);
            }
        }

        private static ApiResult<T> ReadSuccess<T>(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return ApiResult<T>.Success(default(T));

            try
            {
                return ApiResult<T>.Success(JsonConvert.DeserializeObject<T>(body));
            }
            catch (JsonException)
            {
                return ApiResult<T>.Failure("The server returned a response that could not be read.");
            }
        }

        private static ApiResult<T> ReadFailure<T>(string body, HttpResponseMessage response)
        {
            int statusCode = (int)response.StatusCode;
            ProblemDetails problemDetails = null;

            if (!string.IsNullOrWhiteSpace(body))
            {
                try
                {
                    problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(body);
                }
                catch (JsonException)
                {
                    problemDetails = null;
                }
            }

            if (problemDetails == null || string.IsNullOrWhiteSpace(problemDetails.Title))
            {
                return ApiResult<T>.Failure(
                    DescribeStatusCode(response.StatusCode),
                    string.IsNullOrWhiteSpace(body) ? response.ReasonPhrase : body,
                    statusCode);
            }

            return ApiResult<T>.Failure(
                problemDetails.Title,
                string.IsNullOrWhiteSpace(problemDetails.Detail) ? problemDetails.Title : problemDetails.Detail,
                statusCode,
                problemDetails.Errors);
        }

        private static string DescribeStatusCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return "The request was rejected by the server.";
                case HttpStatusCode.Unauthorized:
                    return "Your session has expired. Please sign in again.";
                case HttpStatusCode.Forbidden:
                    return "You do not have permission to perform this action.";
                case HttpStatusCode.NotFound:
                    return "The requested record was not found.";
                case HttpStatusCode.Conflict:
                    return "The record was changed by someone else. Please refresh and try again.";
                case HttpStatusCode.RequestTimeout:
                    return "The server took too long to respond.";
                case HttpStatusCode.InternalServerError:
                    return "The server encountered an unexpected error.";
                case HttpStatusCode.ServiceUnavailable:
                    return "The server is currently unavailable. Please try again later.";
                default:
                    return "The request failed with status " + (int)statusCode + ".";
            }
        }
    }
}
