using DATN_back_end.Dtos.Auth;
using DATN_back_end.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace DATN_back_end.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto userRegister)
        {
            return Ok(await _authService.Register(userRegister));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Ok(await _authService.Login(loginDto));
        }
    }
}