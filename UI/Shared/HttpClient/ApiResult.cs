using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.HttpClient
{
    public class ApiResult<T>
    {
        public string Type { get; set; }
        public int Status { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string TraceId { get; set; }
        public string Title { get; set; }
        public string ErrorCode { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

        public bool DataNotModified
        {
            get { return Status == 304; }
        }

        public string Title_Full
        {
            get
            {
                string fallBackTitle = string.IsNullOrWhiteSpace(Title) ? "The operation could not be completed." : Title.Trim();
                string details = ToErrorMessage();

                return string.IsNullOrWhiteSpace(details)
                    ? fallBackTitle
                    :    details;
            }
        }

        public string ToErrorMessage()
        {
            if (Errors == null || Errors.Count == 0)
                return string.Empty;

            return string.Join(Environment.NewLine, Errors
                .Where(e => e.Value != null && e.Value.Length > 0)
                .SelectMany(e =>// new[] { e.Key }
                  //  .Concat(
                    e.Value.Select(msg =>  msg//)
                    )));
        }

        public static ApiResult<T> NotModified()
        {
            return new ApiResult<T>
            {
                IsSuccess = true,
                Status = 304
            };
        }

        public static ApiResult<T> Success(T data)
        {
            return new ApiResult<T>
            {
                Data = data,
                IsSuccess = true,
                Status = 200
            };
        }

        public static ApiResult<T> Failure(string message, string detail = null, int statusCode = 0,
            Dictionary<string, string[]> validationErrors = null)
        {
            return new ApiResult<T>
            {
                IsSuccess = false,
                Title = message,
                ErrorCode = detail,
                Status = statusCode,
                Errors = validationErrors ?? new Dictionary<string, string[]>()
            };
        }

        public static implicit operator ApiResult<T>(T data)
        {
            return Success(data);
        }

        public static implicit operator ApiResult<T>(string message)
        {
            return Failure(message);
        }
    }
}
