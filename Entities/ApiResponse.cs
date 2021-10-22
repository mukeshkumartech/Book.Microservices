using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Base model class for API response
    /// </summary>
    public class ApiResponse
    {
        public Object Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public static ApiResponse Fail(string errorMessage)
        {
            return new ApiResponse { Succeeded = false, Message = errorMessage };
        }
        public static ApiResponse Success(Object data)
        {
            return new ApiResponse { Succeeded = true, Data = data };
        }

        public static ApiResponse Success(Object data, string successMessage)
        {
            return new ApiResponse { Succeeded = true, Message = successMessage, Data = data };
        }
    }
}
