using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourse.Shared.DTOs
{
    public class Response<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool isSucceeded { get; private set; }

        public List<string> Errors { get; set; }

        public static Response<T> Succeeded(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, isSucceeded = true };
        }

        public static Response<T> Succeeded(int statusCode)
        {
            return new Response<T> { StatusCode = statusCode, isSucceeded = true };
        }


        public static Response<T> Failed(List<string> errors, int statusCode)
        {
            return new Response<T> { Errors = errors, StatusCode = statusCode, isSucceeded = false };
        }

        public static Response<T> Failed(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string> { error }, StatusCode = statusCode, isSucceeded = false };
        }


    }
}
