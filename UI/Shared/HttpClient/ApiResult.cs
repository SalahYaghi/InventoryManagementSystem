using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.HttpClient
{
    public  class ApiResult<T>
    {
        public string Type { get; set; }
        public int Status { get; set; }
        public bool IsSuccess { get; set; } 
        public T Data { get; set; }
        public string TraceId { get; set; }
        public string Title_Full => Title + $"\n{ToErrorMessage()}";
        public string Title { get; set; }   
        public string ErrorCode { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

        public bool DataNotModified => Status == 304;

        public string ToErrorMessage()
        {

            if (Errors != null &&  Errors.Count > 0)
            {

                return string.Join(Environment.NewLine, Errors.SelectMany(e =>
                new[] { e.Key }.Concat(e.Value.Select(msg => $"  - {msg}"))));
            }
            return string.Empty;
        }

        public static ApiResult<T> NotModified()
        {
            return new ApiResult<T>()
            {
                IsSuccess = true ,
                Status = 304 
            };
        }

        public static ApiResult<T> Success(T data) {

            return new ApiResult<T>() { 
            
                 Data = data,
                IsSuccess = true
            
            };
        }
        public static ApiResult<T> Failure(string message, string detail = null, int statusCode = 0, Dictionary<string, string[]> validationErrors = null)
        {
            return new ApiResult<T>
            {
                IsSuccess = false,
                Title = message,
                ErrorCode = detail,
                Status = statusCode,
                Errors = validationErrors
            };
        }

        public static implicit operator ApiResult<T>(T data) => Success(data);
        public static implicit operator ApiResult<T>(string message) => Failure(message);



    }
}

