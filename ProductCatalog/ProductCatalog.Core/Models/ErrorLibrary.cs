using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Models
{
    public static class ErrorLibrary
    {
        public static ErrorModel NotFound(string entityName) =>
            new() { Message = $"{entityName} not found.", ErrorCode = 404 };

        public static ErrorModel Unauthorized() =>
            new() { Message = "Unauthorized access.", ErrorCode = 401 };

        public static ErrorModel ValidationError(string details) =>
            new() { Message = $"Validation failed: {details}", ErrorCode = 400 };

        public static ErrorModel InternalServerError(string details = "") =>
            new() { Message = $"Internal server error. {details}", ErrorCode = 500 };
    }
}
