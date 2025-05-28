using KutuphaneDataAcces.Dtos.Users;
using KutuphaneService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController  : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if(userDto == null)
            {
                return BadRequest("User data cannot be null");
            }

            var response = _userService.CreateUser(userDto);
            if (response.IsSuccess)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            if(loginUserDto == null)
            {
                return BadRequest("Login data cannot be null");
            }

            var response = _userService.LoginUser(loginUserDto);
            if (response.IsSuccess)
            {
                return Ok(response.Message);
            }
            return BadRequest(response.Message);
        }
    }
}
