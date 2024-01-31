using System.Text.Json.Serialization;

namespace NLayer.Core.DTOs
{
    public class CustomResponseModel<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        public List<string> Errors { get; set; }

        public static CustomResponseModel<T> Success(int statusCode, T data)
        {
            return new CustomResponseModel<T> { Data = data, StatusCode = statusCode };

        }
        public static CustomResponseModel<T> Success(int statusCode)
        {
            return new CustomResponseModel<T> { StatusCode = statusCode };

        }
        public static CustomResponseModel<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseModel<T> { StatusCode = statusCode, Errors = errors };

        }
        public static CustomResponseModel<T> Fail(int statusCode, string error)
        {
            return new CustomResponseModel<T> { StatusCode = statusCode, Errors = new List<string> { error} };

        }

    }
}
