using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourse.Shared.DTOs
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool isSucceeded { get; private set; }

        public List<string> Errors { get; set; }

        public static ResponseDto<T> Succeeded(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, isSucceeded = true };
        }

        public static ResponseDto<T> Succeeded(int statusCode)
        {
            return new ResponseDto<T> { StatusCode = statusCode, isSucceeded = true };
        }


        public static ResponseDto<T> Failed(List<string> errors, int statusCode)
        {
            return new ResponseDto<T> { Errors = errors, StatusCode = statusCode, isSucceeded = false };
        }

        public static ResponseDto<T> Failed(string error, int statusCode)
        {
            return new ResponseDto<T> { Errors = new List<string> { error }, StatusCode = statusCode, isSucceeded = false };
        }


    }
}
