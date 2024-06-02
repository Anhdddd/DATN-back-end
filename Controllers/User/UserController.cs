using DATN_back_end.Dtos.User;
using DATN_back_end.Filters;
using DATN_back_end.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace DATN_back_end.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDto userUpdateDto)
        {
            await _userService.UpdateAsync(userUpdateDto);
            return Ok();
        }

        [HttpPut("avatar")]
        public async Task<IActionResult> UpdateAvatarAsync( IFormFile file)
        {
            await _userService.UpdateAvatarAsync(file);
            return Ok();
        }

        [HttpGet("my-info")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _userService.GetMyInfo();
            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }
    }
}
