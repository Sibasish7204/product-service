using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Authorization.Interface;
using ProductCatalog.Core.DTO;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductCatalog.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DtoUserLogIn user)
        {
            var dbUser = user.Adapt<User>();
            dbUser = await _userService.GetUserByEmail(user.Email);
            
            if (user.Email == dbUser.Email && user.Password == dbUser.Password)
            {
                var token = _tokenService.GenerateToken(user.Email, user.Password);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("authenticate")]

        public async Task<IActionResult> ValidateUser()
        {

            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userService.GetUserByEmail(email);
            if(user != null) { return Ok(); }
            return Unauthorized();
        }
    }

}
