using Application.Repositories;
using Application.Services.CategoryService.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;
        public CategoryService(IGenericRepository<Category> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task CreateCategory(CreateCategoryDto input)
        {
            if (await _genericRepository.GetAll().AnyAsync(x => x.Name == input.Name.ToLower().Trim()))
            {
                throw new Exception("This Category Already Exist!");
            }

            var data = new Category
            {
                Name = input.Name,
                Description = input.Description
            };

            await _genericRepository.InsertAsync(data);
            await _genericRepository.SaveChangesAsync();

        }

        public async Task DeleteCategory(Guid Id)
        {
            var data = await _genericRepository.GetByIdAsync(Id);
            _genericRepository.Delete(data);
            await _genericRepository.SaveChangesAsync();

        }

        public async Task<List<GetCategoriesDto>> GetCategories(string? name)
        {
            name = !string.IsNullOrEmpty(name) ? name.ToLower().Trim() : null;

            var data = _genericRepository.GetAll();
            if (name != null)
            {
                data = data.Where(x => x.Name.ToLower().Trim().Contains(name));
            }

            var result = data.Select(data => new GetCategoriesDto
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            }).ToList();

            return result;
        }

        public async Task<GetCategoriesDto> GetCategoryById(Guid Id)
        {
            var data = await _genericRepository.GetByIdAsync(Id);
            var result = new GetCategoriesDto
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };

            return result;
        }
    }
}
