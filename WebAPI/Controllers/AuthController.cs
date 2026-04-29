using Application.Services.AuthService;
using Application.Services.AuthService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto input)
        {
            var result = await _authService.Login(input);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto? input)
        {
            await _authService.LogoutAsync(input?.Token);
            return Ok();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto input)
        {
            var newToken = await _authService.RefreshToken(input);
            if (newToken == null) return Unauthorized("Invalid or expired refresh token");

            return Ok();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto input)
        {
            await _authService.ChangeUserPassword(input);
            return Ok();
        }
    }
}
