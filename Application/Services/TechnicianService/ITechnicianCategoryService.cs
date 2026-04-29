using Application.Services.TechnicianService.DTOs;

namespace Application.Services.TechnicianService
{
    public interface ITechnicianCategoryService
    {
        Task AssignCategoryToTechnician(Guid userId, Guid categoryId);
        Task<List<AssignCategoryToTechnicianDto>> GetTechnicianCategories(Guid userId);
    }
}
