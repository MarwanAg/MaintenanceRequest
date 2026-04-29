using Application.Services.CategoryService;
using Application.Services.CategoryService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryServiceRepo;
        public CategoryController(ICategoryService categoryServiceRepo)
        {
            _categoryServiceRepo = categoryServiceRepo;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories(string? name)
        {
            var data = await _categoryServiceRepo.GetCategories(name);
            return Ok(data);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid Id)
        {
            var data = await _categoryServiceRepo.GetCategoryById(Id);
            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto input)
        {
            await _categoryServiceRepo.CreateCategory(input);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(Guid Id)
        {
            _categoryServiceRepo.DeleteCategory(Id);
            return Ok();
        }
    }
}
