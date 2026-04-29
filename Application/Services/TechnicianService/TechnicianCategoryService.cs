using Application.Repositories;
using Application.Services.TechnicianService.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.TechnicianService
{
    public class TechnicianCategoryService : ITechnicianCategoryService
    {
        private readonly IGenericRepository<TechnicianCategory> _techCategoryRepo;
        private readonly IGenericRepository<User> _userRepository;

        public TechnicianCategoryService(IGenericRepository<TechnicianCategory> techCategoryRepo, IGenericRepository<User> userRepository)
        {
            _techCategoryRepo = techCategoryRepo;
            _userRepository = userRepository;
        }

        public async Task AssignCategoryToTechnician(Guid userId, Guid categoryId)
        {
            var user = await _userRepository.GetAll()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) {
                throw new Exception("User not found");
            }
            if (user.Role.Code != SystemRole.Technician)
            {
                throw new Exception("Only users with 'Technician' role can be assigned to categories.");
            }

            var isExist = await _techCategoryRepo.GetAll().AnyAsync(x => x.TechnicianId == userId && x.CategoryId == categoryId);

            if (!isExist)
            {
                var result = new TechnicianCategory
                {
                    Id = Guid.NewGuid(),
                    TechnicianId = userId,
                    CategoryId = categoryId
                };
                await _techCategoryRepo.InsertAsync(result);
                await _techCategoryRepo.SaveChangesAsync();
            }
        }

        public async Task<List<AssignCategoryToTechnicianDto>> GetTechnicianCategories(Guid userId)
        {
            var categories = await _techCategoryRepo.GetAll().Where(x => x.TechnicianId == userId)
                .Select(x => new AssignCategoryToTechnicianDto
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    TechnicianId = x.TechnicianId
                }).ToListAsync();

            return categories;
        }
    }
}
