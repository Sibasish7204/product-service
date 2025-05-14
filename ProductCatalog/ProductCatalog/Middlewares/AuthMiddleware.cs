using ProductCatalog.Data.Interfaces;
using System.Security.Claims;

namespace ProductCatalog.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public AuthMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            if (!context.Request.Path.Value.ToLower().Contains("api/auth/login"))
            {
                var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
                if (email != null)
                {
                    var user = await userRepository.GetUserByEmail(email);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid token: user not found.");
                    return;
                }
            }

            await _requestDelegate(context);
        }
    }
}
