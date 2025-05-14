using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Models
{
    public class GenericResponseModel<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }
        public List<ErrorModel>? Errors { get; init; }

        public static GenericResponseModel<T> SuccessResponse(T data, string message = "Success")
        => new() { Success = true, Message = message, Data = data };

        public static GenericResponseModel<T> FailureResponse(List<ErrorModel> errors, string message = "Failure")
            => new() { Success = false, Message = message, Errors = errors };
    }

    public class ErrorModel
    {
        public string Message { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
    }
}
