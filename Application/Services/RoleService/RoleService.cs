using Application.Repositories;
using Application.Services.RoleService.DTOs;
using Domain.Entities;

namespace Application.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _genericRepository;
        public RoleService(IGenericRepository<Role> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public List<GetRolesDto> GetAllRoles()
        {
            var data =  _genericRepository.GetAll();
            var result = data.Select(x => new GetRolesDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return result;
        }
    }
}
