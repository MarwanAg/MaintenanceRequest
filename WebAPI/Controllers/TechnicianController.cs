using Application.Services.TechnicianService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianCategoryService _technicianService;

        public TechnicianController(ITechnicianCategoryService technicianService)
        {
            _technicianService = technicianService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{userId}/categories/{categoryId}")]
        public async Task<IActionResult> AssignCategory(Guid userId, Guid categoryId)
        {
            await _technicianService.AssignCategoryToTechnician(userId, categoryId);
            return Ok();
        }

        [HttpGet("{userId}/categories")]
        public async Task<IActionResult> GetCategories(Guid userId)
        {
            var result = await _technicianService.GetTechnicianCategories(userId);
            return Ok(result);
        }
    }
}
