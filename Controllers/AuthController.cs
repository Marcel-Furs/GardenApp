using GardenApp.API.Attributes;
using GardenApp.API.Dto;
using GardenApp.API.Exceptions;
using GardenApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.API.Controllers
{
    [GardenAppV1Route]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        public AuthController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userService.RegisterUser(userDto.Username, userDto.Password);
                }
                catch (InvalidCredentialsException ex)
                {
                    return BadRequest(ex);
                }

                return Ok(new { status = "created" });
            }

            return BadRequest("Invalid body data");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = userService.AuthUser(userDto.Username, userDto.Password);
                    return Ok(new { Token = tokenService.CreateToken(userId.ToString(), userDto.Username) });
                }
                catch (InvalidCredentialsException ex)
                {
                    return Unauthorized(ex.Message);
                }
            }

            return BadRequest("Invalid body data");
        }
    }
}
