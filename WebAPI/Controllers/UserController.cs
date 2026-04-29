using Application.Services.UserService;
using Application.Services.UserService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServiceRepo;
        public UserController(IUserService userServiceRepo)
        {
            _userServiceRepo = userServiceRepo;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(string? username, string? email)
        {
            var data = await _userServiceRepo.GetAllUsers(username, email);
            return Ok(data);
        }
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid Id)
        {
            var data = await _userServiceRepo.GetUserById(Id);
            return Ok(data);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto input)
        {
            await _userServiceRepo.CreateUser(input);
            return Ok();
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid Id, UpdateUserDto input)
        {
            await _userServiceRepo.UpdateUser(Id, input);
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            await _userServiceRepo.DeleteUser(Id);
            return Ok();
        }
    }
}
