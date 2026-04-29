using Application.Services.CategoryService.DTOs;

namespace Application.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<GetCategoriesDto>> GetCategories(string? name);
        Task<GetCategoriesDto> GetCategoryById(Guid Id);
        Task CreateCategory(CreateCategoryDto input);
        Task DeleteCategory(Guid Id);
    }
}
