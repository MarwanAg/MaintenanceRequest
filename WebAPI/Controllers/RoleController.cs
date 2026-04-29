using Application.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleServiceRepo;
        public RoleController(IRoleService roleServiceRepo)
        {
            _roleServiceRepo = roleServiceRepo;
        }

        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            var data = _roleServiceRepo.GetAllRoles();
            return Ok(data);
        }
    }
}
