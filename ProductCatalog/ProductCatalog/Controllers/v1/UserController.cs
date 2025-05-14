using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.DTO;
using ProductCatalog.Data.DbModels;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(DtoUserSignUp userSignUp)
        {
            var user = userSignUp.Adapt<User>();
            await _userService.Register(user);

            return(Ok(user));
        }
    }
}
