using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementDemo.API.DTOs;
using StockManagementDemo.API.Interfaces;

namespace StockManagementDemo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService auth) : ControllerBase
    {
        private readonly IAuthService _auth = auth;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _auth.UserExistsAsync(dto.Username))
                return BadRequest("Username already exists.");

            var user = await _auth.RegisterUserAsync(dto);
            return Ok(new { user.Id, user.Username, user.Email });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _auth.AuthenticateUserAsync(dto);
            if (token is null)
                return Unauthorized("Invalid username or password.");

            return Ok(new { token });
        }
    }
}