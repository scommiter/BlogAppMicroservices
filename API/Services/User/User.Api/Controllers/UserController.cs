using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.User;
using User.Api.GrpcService;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserGrpcService _userGrpcService;
        public UserController(UserGrpcService userGrpcService)
        {
            _userGrpcService = userGrpcService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            await _userGrpcService.Register(userDto);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            var model = await _userGrpcService.Login(userDto);
            return Ok(model);
        }
    }
}
